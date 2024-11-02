using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Vector2 cursorReleasePos;
    Rigidbody2D rb;
    bool gameOver;
    [SerializeField]
    Transform lavaTrans, miniMapCamTrans;

    [SerializeField]
    PlayerConfig playerConfig;

    // Configs
    private float energyForce, maxExplodeForce,
    minEnergyNeeded, energyNeedMul,
    initialFuelPower, energyBurnOff,
    energyBurnOffSlowMo, initialMaxEnergy, 
    maxLavaDistance, lavaSpeed, maxEnergyIncreasePerLv,
    timeSlowAmount, fuelPowerIncreasePerLv,
    energyDurabilityIncreasePerLv;

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

    GameManager gameManager {
        get {return GameManager.instance;}
    }

    // Start is called before the first frame update
    void Start(){
        SetUpConfiguration();

        gameOver = false;
        coinCount = 0;
        alreadyPaused = false;
        levelWhenGameStart = gameManager.gameLevel;
        currentLevel = levelWhenGameStart;
        maxEnergy = gameManager.maxEnergyLv*maxEnergyIncreasePerLv+initialMaxEnergy;
        energyRemaining = maxEnergy;
        energyRefuel = initialFuelPower + gameManager.fuelPowerLv*fuelPowerIncreasePerLv;
        energyWastePercentage = (100-gameManager.energyDurabilityLv*energyDurabilityIncreasePerLv)/100;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        rb = GetComponent<Rigidbody2D>();
        var skin = gameManager.currentSkin;
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
        energyRemaining -= energyWastePercentage*Time.deltaTime/(1/energyBurnOff);
        UpdateHeightIndicator();
        gameManager.inGameCoinCnt = coinCount;
        slider.fillAmount = energyRemaining/maxEnergy;

        // gameover
        if (transform.position.y <= lavaTrans.position.y + 15.5 & !gameOver){
            gameOverPanel.SetActive(true);
            gameManager.coinCount += coinCount*gameManager.gameLevel;
            gameManager.SaveGame();
            // rb.bodyType = RigidbodyType2D.Static;
            Time.timeScale = 0;
            gameOver = true;
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
            Time.timeScale = 1/timeSlowAmount;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            foreach (var dot in dots){
                dot.SetActive(true);
            }
        }
        if(Input.GetMouseButton(0)){
            OnCursorPress();
            energyRemaining -= energyWastePercentage*Time.deltaTime/(1/(energyBurnOff*energyBurnOffSlowMo));
        }
        if(Input.GetMouseButtonUp(0)){
            OnCursorRelease();
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            foreach (var dot in dots){
                dot.SetActive(false);
            }
        }
    }

    void SetUpConfiguration(){
        energyForce = playerConfig.energyForce;
        maxExplodeForce = playerConfig.maxExplodeForce;
        minEnergyNeeded = playerConfig.minEnergyNeeded;
        energyNeedMul = playerConfig.energyNeedMul;
        initialFuelPower = playerConfig.initialFuelPower;
        energyBurnOff = playerConfig.energyBurnOff;
        energyBurnOffSlowMo = playerConfig.energyBurnOffSlowMo;
        lavaSpeed = playerConfig.lavaSpeed;
        maxLavaDistance = playerConfig.maxLavaDistance;
        initialMaxEnergy = playerConfig.initialMaxEnergy;
        maxEnergyIncreasePerLv = playerConfig.maxEnergyIncreasePerLv;
        timeSlowAmount = playerConfig.timeSlowAmount;
        fuelPowerIncreasePerLv = playerConfig.fuelPowerIncreasePerLv;
        energyDurabilityIncreasePerLv = playerConfig.energyDuribilityIncreasePerLv;
    }

    void OnCursorPress(){
        cursorReleasePos += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        // Debug.Log("Cursor pressed at : " + cursorReleasePos);
        ShowProjectile();
    }

    void UpdateLava(){
        if ((transform.position.y - lavaTrans.position.y) < maxLavaDistance + 15){
            lavaTrans.position = new Vector2(transform.position.x, lavaTrans.position.y + lavaSpeed*Time.deltaTime/Time.timeScale);

        }
        else{
            lavaTrans.position = new Vector2(transform.position.x, transform.position.y-(maxLavaDistance + 15));
        }
    }

    void UpdateHeightIndicator(){
        heightSlider.fillAmount = transform.position.y / 1000;
        heightText.rectTransform.localPosition = new Vector3(heightText.rectTransform.localPosition.x, heightSlider.fillAmount * 600 - 300, heightText.rectTransform.localPosition.z);
        heightText.text = ((int)(transform.position.y+(levelWhenGameStart-1)*1000)).ToString();
    }

    void UpdateLevel(){
        var level = levelWhenGameStart + (int)(transform.position.y/1000);
        if (currentLevel < level){
            var levelDiff = level - currentLevel;
            for (var i = 0; i < levelDiff; i++){
                gameManager.gameLevel++;
                generator.LevelUp();
                currentLevel++;
            }
        }
    }

    void ShowProjectile(){
        var velocity = new Vector2();
        var CurrentVelocity = rb.linearVelocity/2;
        var energyNeed = minEnergyNeeded+(energyNeedMul * cursorReleasePos.magnitude);
        var direction = (-cursorReleasePos).normalized;
        if(energyNeed > energyRemaining){
            velocity = CurrentVelocity + direction*energyRemaining*energyForce/50;
        }
        else{
            velocity = CurrentVelocity + direction*energyNeed*energyForce/50;
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
        var energyNeed = minEnergyNeeded+(energyNeedMul * cursorReleasePos.magnitude);
        var direction = (-cursorReleasePos).normalized;
        if(energyNeed > energyRemaining){
            rb.AddForce(direction*energyRemaining*energyForce);
            energyRemaining = 0;
            cursorReleasePos = Vector2.zero;
            return;
        }
        rb.AddForce(direction*energyNeed*energyForce);
        // Debug.Log("Cursor released at : " + cursorReleasePos);
        energyRemaining -= energyNeed*energyWastePercentage;
        cursorReleasePos = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Energy"){
            var explodeForce = new Vector2(Random.Range(-maxExplodeForce, maxExplodeForce), Random.Range(-maxExplodeForce/5, maxExplodeForce));
            rb.AddForce(explodeForce, ForceMode2D.Impulse);
            generator.RemoveObject(other.transform, EObjectType.Energy);
            energyRemaining += energyRefuel;
        }
        if (other.gameObject.tag == "Bomb"){
            var explodeForce = new Vector2(Random.Range(-maxExplodeForce, maxExplodeForce), Random.Range(-maxExplodeForce/5, maxExplodeForce));
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
