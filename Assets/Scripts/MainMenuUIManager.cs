using UnityEngine;
using MyFramework;

public class MainMenuUIManager : MonoBehaviourSimplify
{
    private GUIManager guiMgr;
    private GameManager gameManager;
    [SerializeField]
    BGMManager bgmManager;
    UserData userData {
        get {
            Debug.Log(UserData.userData.name);
            return UserData.userData;}
    }
    private void Awake() {
        guiMgr = GUIManager.instance;
        gameManager = GameManager.instance;
        guiMgr.Set(new Vector2(2160, 1080), 1);
        guiMgr.RemoveAllPanel();
    }
    private void Start() {
        if (gameManager.cameFromGame){
            OnStartBtn();
            return;
        }
        var titleMenu = guiMgr.AddPanel("TitleMenu", ELayer.Bottom);
        titleMenu.GetComponent<TitleMenuComponents>().GetStartBtn();
        guiMgr.OnClick(titleMenu.GetComponent<TitleMenuComponents>().GetStartBtn(), OnStartBtn);
        guiMgr.OnClick(titleMenu.GetComponent<TitleMenuComponents>().GetExitBtn(), OnExitBtn);
        print(CoinNeedToLevelUp(50));
    }

    private void OnStartBtn(){
        guiMgr.RemovePanel("TitleMenu");
        var mainMenuComponents = guiMgr.AddPanel("MainMenu", ELayer.Middle).GetComponent<MainMenuComponents>();
        guiMgr.OnClick(mainMenuComponents.startGameBtn, OnStartGameBtn);
        foreach (var skin in SkinList.defaultSkinList.list){
            var btnScript = Instantiate(Resources.Load<GameObject>("SkinBtn"), mainMenuComponents.skinContent)
                                .GetComponent<SkinBtn>();
            btnScript.skin = skin;
            guiMgr.OnClick(btnScript.btn, btnScript.OnClick);
            btnScript.rawImage.texture = skin.thumbnail;
            if (userData.purchasedSkins.Contains(skin)){
                btnScript.costText.text = "";
            }
            else{
                btnScript.rawImage.color = new Color(.5f, .5f, .5f);
                btnScript.costText.text = skin.cost.ToString();
            }
        }
        
        guiMgr.OnClick(mainMenuComponents.settingsBtn, OnSettingsBtn);
        guiMgr.OnClick(mainMenuComponents.addMaxEnergyBtn, OnAddMaxEnergyBtn);
        guiMgr.OnClick(mainMenuComponents.addEnergyRefuelBtn, OnAddEnergyRefuelBtn);
        guiMgr.OnClick(mainMenuComponents.addEnergyWasteBtn, OnAddEnergyWasteBtn);

        RefreshUI();
    }

    private void RefreshUI(){
        var menuComponents = guiMgr.GetPanel("MainMenu").GetComponent<MainMenuComponents>();
        menuComponents.coinCntTxt.text = userData.coinCount.ToString();

        menuComponents.highestScoreTxt.text = "Highest score: " + userData.highestScore.ToString();

        // Set max enery level texts
        menuComponents.maxEnergyLvTxt.text = "Lv. " + userData.maxEnergyLv.ToString();
        menuComponents.coin4MaxEnergyTxt.text = CoinNeedToLevelUp(userData.maxEnergyLv).ToString();

        // Set enery refuel level texts
        menuComponents.energyRefuelLvTxt.text = "Lv. " + userData.fuelPowerLv.ToString();
        menuComponents.coin4EnergyRefuelTxt.text = CoinNeedToLevelUp(userData.fuelPowerLv).ToString();

        // Set enery waste level texts
        menuComponents.energyWasteLvTxt.text = "Lv. " + userData.energyDurabilityLv.ToString();
        menuComponents.coin4EnergyWasteTxt.text = CoinNeedToLevelUp(userData.energyDurabilityLv).ToString();
        
        menuComponents.gameLevelTxt.text = "Level: " + userData.gameLevel;
    }

    private void OnExitBtn(){
        gameManager.SaveGame();
        Application.Quit();
    }

    private void OnStartGameBtn(){
        guiMgr.RemoveAllPanel();
        gameManager.SaveGame();
        bgmManager.Play();
        LevelMgr.instance.LoadScene("GameScene");
    }

    private void OnSettingsBtn(){
        switch (EnvironmentConfig.environment.mode){
            case EnvironmentMode.Developing:
                var settingsPanelComponentsDev = guiMgr.AddPanel("SettingsPanelDev", ELayer.Top)
                                                .GetComponent<SettingsPanelComponents>();
                guiMgr.OnClick(settingsPanelComponentsDev.xBtn, OnSettingsXBtn);
                guiMgr.OnClick(settingsPanelComponentsDev.addCoinBtn, OnAddCoinBtn);
                guiMgr.OnClick(settingsPanelComponentsDev.setLevelBtn, OnSetLevelBtn);
                guiMgr.OnClick(settingsPanelComponentsDev.resetDataBtn, OnResetDataBtn);
                guiMgr.OnClick(settingsPanelComponentsDev.saveGameBtn, OnSaveGameBtn);
                break;
            case EnvironmentMode.Testing or EnvironmentMode.Release:
                var settingsPanelComponents = guiMgr.AddPanel("SettingsPanelRelease", ELayer.Top)
                                                .GetComponent<SettingsPanelComponents>();
                guiMgr.OnClick(settingsPanelComponents.xBtn, OnSettingsXBtn);
                guiMgr.OnClick(settingsPanelComponents.saveGameBtn, OnSaveGameBtn);
                break;
        }
    }

    private void OnSaveGameBtn()
    {
        gameManager.SaveGame();
    }

    private void OnResetDataBtn()
    {
        gameManager.ResetData();
        RefreshUI();
        BroadcastEvent("OnResetData", this);
    }

    private void OnSetLevelBtn()
    {
        var value = guiMgr.GetPanel("SettingsPanelDev").GetComponent<SettingsPanelComponents>().setLevelIF.text;
        int num;
        if (int.TryParse(value, out num)){
            userData.gameLevel = num;
        }
        guiMgr.GetPanel("MainMenu")
        .GetComponent<MainMenuComponents>()
        .gameLevelTxt.text = "Level: " + userData.gameLevel.ToString();
    }

    private void OnAddCoinBtn(){
        var value = guiMgr.GetPanel("SettingsPanelDev").GetComponent<SettingsPanelComponents>().addCoinIF.text;
        int num;
        if (int.TryParse(value, out num)){
            userData.coinCount += num;
        }
        guiMgr.GetPanel("MainMenu")
        .GetComponent<MainMenuComponents>()
        .coinCntTxt.text = userData.coinCount.ToString();
    }

    private void OnSettingsXBtn(){
        switch (EnvironmentConfig.environment.mode){
            case EnvironmentMode.Developing:
                guiMgr.RemovePanel("SettingsPanelDev");
                break;
            case EnvironmentMode.Testing or EnvironmentMode.Release:
                guiMgr.RemovePanel("SettingsPanelRelease");
                break;
        }
    }

    private void OnAddMaxEnergyBtn() {
        
        var coinNeed = CoinNeedToLevelUp(userData.maxEnergyLv);
        if (coinNeed > userData.coinCount) {
            return;
        }
        userData.maxEnergyLv += 1;
        userData.coinCount -= coinNeed;
        RefreshUI();
    }

    private void OnAddEnergyRefuelBtn() {
        
        var coinNeed = CoinNeedToLevelUp(userData.fuelPowerLv);
        if (coinNeed > userData.coinCount) {
            return;
        }
        userData.fuelPowerLv += 1;
        userData.coinCount -= coinNeed;
        RefreshUI();
    }

    private void OnAddEnergyWasteBtn() {
        
        var coinNeed = CoinNeedToLevelUp(userData.energyDurabilityLv);
        if (coinNeed > userData.coinCount) {
            return;
        }
        userData.energyDurabilityLv += 1;
        userData.coinCount -= coinNeed;
        RefreshUI();
    }

    int CoinNeedToLevelUp(int currentLevel){
        if (currentLevel == 0) {
            return 1;
        }
        var sum = 0;
        for (int i = 1; i <= currentLevel; i++){
            sum += i;
        }
        return CoinNeedToLevelUp(currentLevel-1)+sum;
    }
}
