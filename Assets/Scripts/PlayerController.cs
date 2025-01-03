using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apple.Core;
using Apple.GameKit;
using Apple.GameKit.Leaderboards;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Vector2 cursorReleasePos;
    Rigidbody2D rb;
    bool gameOver;
    [SerializeField]
    Transform lavaTrans, miniMapCamTrans;
    [SerializeField]
    PostProcessVolume ppvNormal;

    [SerializeField]
    PlayerConfig playerConfig;

    [SerializeField]
    AudioSource audioSource;

    GKAchievement intoTheSpace;
    GKLeaderboard highScore;


    private float energyRemaining;

    private float energyWastePercentage, maxEnergy, energyRefuel;

    [SerializeField]
    EnergyGenerator generator;

    [SerializeField]
    Image slider, heightSlider;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    TMPro.TMP_Text heightText, goldCountText;

    int coinCount, levelWhenGameStart, currentLevel;

    bool alreadyPaused;
    
    [SerializeField]    
    List<GameObject> dots;

    UserData userData{
        get {return UserData.userData;}
    }

    GameManager gameManager {
        get {return GameManager.instance;}
    }

    // Start is called before the first frame update
    void Start(){
        GetAchievements();
        GetLeaderboards();
        SetUpVariables();
    }

    // Update is called once per frame
    void Update()
    {
        miniMapCamTrans.position = new Vector3(transform.position.x, transform.position.y, miniMapCamTrans.position.z);
        if (gameOver) return;
        if (gameManager.gamePaused){
            if (!alreadyPaused){
                alreadyPaused = true;
            }
            Time.timeScale = 0;
            return;
        }
        
        if (alreadyPaused){
            // rb.linearVelocity = velocityBeforePause;
            Time.timeScale = 1;
            alreadyPaused = false;
        }

        UpdateLava();
        energyRemaining -= energyWastePercentage*Time.deltaTime/(1/playerConfig.energyBurnOff);
        UpdateHeightIndicator();
        slider.fillAmount = energyRemaining/maxEnergy;
        CheckAchievements();

        // gameover
        if (transform.position.y <= lavaTrans.position.y + 15.5 & !gameOver){
            GameOver();
            return;
        }

        if (energyRemaining <= 0){
            energyRemaining = 0;
            return;
        }

        UpdateLevel();

        if (energyRemaining > maxEnergy){
            energyRemaining = maxEnergy;
        }
        if(Input.GetMouseButtonDown(0)){
            Time.timeScale = 1/playerConfig.timeSlowAmount;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            StopAllCoroutines();
            foreach (var dot in dots){
                dot.SetActive(true);
            }
        }
        if(Input.GetMouseButton(0)){
            OnCursorPress();
            energyRemaining -= energyWastePercentage*Time.deltaTime/(1/(playerConfig.energyBurnOff*playerConfig.energyBurnOffSlowMo));
            ppvNormal.weight = Mathf.Lerp(ppvNormal.weight, 0, (1/playerConfig.sloMoEffectFadeInTime)*(Time.deltaTime/Time.timeScale));
        }
        if(Input.GetMouseButtonUp(0)){
            OnCursorRelease();
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            foreach (var dot in dots){
                dot.SetActive(false);
            }
            StartCoroutine(ReversePPV());
        }
    }

    void GameOver(){
        SaveGame();
        UpdateLeaderboard();
        // rb.bodyType = RigidbodyType2D.Static;
        Time.timeScale = 0;
        gameOver = true;
    }

    public void UpdateLeaderboard(){
        AppleGameCenter.instance.SubmitNewScore(highScore, (long)(transform.position.y+(levelWhenGameStart-1)*1000), 0);
    }

    public void SaveGame(){
        gameOverPanel.SetActive(true);
        userData.coinCount += coinCount*userData.gameLevel;
        var currentScore = (int)(transform.position.y+(levelWhenGameStart-1)*1000);
        userData.highestScore = userData.highestScore > currentScore ? userData.highestScore : currentScore;
        gameManager.SaveGame();
    }

    void SetUpVariables(){
        gameOver = false;
        coinCount = 0;
        alreadyPaused = false;
        levelWhenGameStart = userData.gameLevel;
        currentLevel = levelWhenGameStart;
        maxEnergy = userData.maxEnergyLv*playerConfig.maxEnergyIncreasePerLv+playerConfig.initialMaxEnergy;
        energyRemaining = maxEnergy;
        energyRefuel = playerConfig.initialFuelPower + userData.fuelPowerLv*playerConfig.fuelPowerIncreasePerLv;
        energyWastePercentage = (100-userData.energyDurabilityLv*playerConfig.energyDurabilityIncreasePerLv)/100;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        rb = GetComponent<Rigidbody2D>();
        var skin = userData.currentSkin;
        if (skin.haveTrail){
            var trailRenderer = GetComponent<TrailRenderer>();
            trailRenderer.enabled = true;
            trailRenderer.startColor = skin.trailColor;
            trailRenderer.endColor = new Color(skin.trailColor.r, skin.trailColor.g, skin.trailColor.b, 0f);
        }
        else{
            GetComponent<TrailRenderer>().enabled = false;
        }
        
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = skin.sprite;
        renderer.color = skin.spriteColor;
        gameOverPanel.SetActive(false);
        foreach (var Go in dots){
            Go.SetActive(false);
        }
        ppvNormal.weight = 1f;
    }

    void OnCursorPress(){
        cursorReleasePos += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        // Debug.Log("Cursor pressed at : " + cursorReleasePos);
        ShowProjectile();
    }

    void UpdateLava(){
        if ((transform.position.y - lavaTrans.position.y) < playerConfig.maxLavaDistance + 15){
            lavaTrans.position = new Vector2(transform.position.x, lavaTrans.position.y + playerConfig.lavaSpeed*Time.deltaTime/Time.timeScale);

        }
        else{
            lavaTrans.position = new Vector2(transform.position.x, transform.position.y-(playerConfig.maxLavaDistance + 15));
        }
    }

    void UpdateHeightIndicator(){
        heightSlider.fillAmount = transform.position.y / 1000;
        heightText.rectTransform.localPosition = new Vector3(heightText.rectTransform.localPosition.x, heightSlider.fillAmount * 600 - 300, heightText.rectTransform.localPosition.z);
        heightText.text = ((int)(transform.position.y+(levelWhenGameStart-1)*1000)).ToString();
    }

    async void GetAchievements(){
        intoTheSpace = await AppleGameCenter.instance.GetAchievementAsync("1001");
    }

    async void GetLeaderboards(){
        highScore = await AppleGameCenter.instance.GetLeaderboardAsync("0001");
    }

    void CheckAchievements(){
        if (intoTheSpace == null)
            return;
        if (!intoTheSpace.IsCompleted){
            var currentPercentage = (int)(transform.position.y+(levelWhenGameStart-1)*1000)/10;
            if (currentPercentage>=100){
                intoTheSpace.PercentComplete = 100;
                intoTheSpace.ShowCompletionBanner = true;
                AppleGameCenter.instance.ReportAchievement(intoTheSpace);
            }
        }
    }

    void UpdateLevel(){
        var level = levelWhenGameStart + (int)(transform.position.y/1000);
        if (currentLevel < level){
            var levelDiff = level - currentLevel;
            for (var i = 0; i < levelDiff; i++){
                userData.gameLevel++;
                generator.LevelUp();
                currentLevel++;
            }
        }
    }

    void ShowProjectile(){
        var velocity = new Vector2();
        var CurrentVelocity = rb.linearVelocity/2;
        var energyNeed = playerConfig.minEnergyNeeded+(playerConfig.energyNeedMul * cursorReleasePos.magnitude);
        var direction = (-cursorReleasePos).normalized;
        if(energyNeed > energyRemaining){
            velocity = CurrentVelocity + direction*energyRemaining*playerConfig.energyForce/50;
        }
        else{
            velocity = CurrentVelocity + direction*energyNeed*playerConfig.energyForce/50;
        }
        float t = 0.0f;
        foreach(var dot in dots){
            var x = velocity.x*t+transform.position.x;
            var y = velocity.y*t+0.5f*(-9.81f)*t*t+transform.position.y;
            // Debug.Log("velocity: "+velocity+" x: "+x+" y: "+y);
            dot.transform.position = new Vector3(x, y);
            t += 0.2f;
        }
    }

    void OnCursorRelease(){
        rb.linearVelocity = rb.linearVelocity/2;
        var energyNeed = playerConfig.minEnergyNeeded+(playerConfig.energyNeedMul * cursorReleasePos.magnitude);
        var direction = (-cursorReleasePos).normalized;
        if(energyNeed > energyRemaining){
            rb.AddForce(direction*energyRemaining*playerConfig.energyForce);
            energyRemaining = 0;
            cursorReleasePos = Vector2.zero;
            return;
        }
        rb.AddForce(direction*energyNeed*playerConfig.energyForce);
        // Debug.Log("Cursor released at : " + cursorReleasePos);
        energyRemaining -= energyNeed*energyWastePercentage;
        cursorReleasePos = Vector2.zero;
    }
    IEnumerator ReversePPV(){
        while (ppvNormal.weight < 1){
            ppvNormal.weight = Mathf.Lerp(ppvNormal.weight, 1, (1/playerConfig.sloMoEffectFadeInTime)*(Time.deltaTime/Time.timeScale));
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Energy"){
            var explodeForce = new Vector2
                (UnityEngine.Random.Range(-playerConfig.maxExplodeForce, playerConfig.maxExplodeForce), 
                UnityEngine.Random.Range(-playerConfig.maxExplodeForce/5, playerConfig.maxExplodeForce)
            );
            rb.AddForce(explodeForce, ForceMode2D.Impulse);
            generator.RemoveObject(other.transform, EObjectType.Energy);
            energyRemaining += energyRefuel;
            audioSource.Play();
        }
        if (other.gameObject.tag == "Bomb"){
            var explodeForce = new Vector2(
                UnityEngine.Random.Range(-playerConfig.maxExplodeForce, playerConfig.maxExplodeForce),
                UnityEngine.Random.Range(-playerConfig.maxExplodeForce/5, playerConfig.maxExplodeForce)
            );
            rb.AddForce(explodeForce, ForceMode2D.Impulse);
            generator.RemoveObject(other.transform, EObjectType.Bomb);
            energyRemaining -= 30;
        }
    }

    // TODO: Find out should coin provide energy
    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("On trigger enter");
        if (other.gameObject.tag == "Coin"){
            energyRemaining += energyRefuel/2;
            coinCount++;
            generator.RemoveObject(other.transform, EObjectType.Coin);
            goldCountText.text = coinCount.ToString();
        }
    }
}
