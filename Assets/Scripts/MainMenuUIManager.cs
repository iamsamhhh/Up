using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyFramework;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviourSimplify
{
    private GUIManager guiMgr;
    private GameManager gameManager;
    [SerializeField]
    BGMManager bgmManager;
    UserData userData {
        get {return UserData.defaultUserData;}
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
        guiMgr.OnClick(mainMenuComponents.GetStartGameBtn(), OnStartGameBtn);
        foreach (var skin in userData.skinList.skinList){
            var btnScript = Instantiate(Resources.Load<GameObject>("SkinBtn"), mainMenuComponents.skinContent)
                                .GetComponent<SkinBtn>();
            btnScript.skin = skin;
            guiMgr.OnClick(btnScript.btn, btnScript.OnClick);
            btnScript.rawImage.texture = skin.thumbnail;
            btnScript.costText.text = skin.cost.ToString();
        }
        
        guiMgr.OnClick(mainMenuComponents.GetSettingsBtn(), OnSettingsBtn);
        guiMgr.OnClick(mainMenuComponents.GetAddMaxEnergyBtn(), OnAddMaxEnergyBtn);
        guiMgr.OnClick(mainMenuComponents.GetAddEnergyRefuelBtn(), OnAddEnergyRefuelBtn);
        guiMgr.OnClick(mainMenuComponents.GetAddEnergyWasteBtn(), OnAddEnergyWasteBtn);

        mainMenuComponents.GetCoinCntTxt().text = userData.coinCount.ToString();

        mainMenuComponents.GetHighestScoreTxt().text = "Highest score: " + userData.highestScore.ToString();

        // Set max enery level texts
        mainMenuComponents.GetMaxEnergyLvTxt().text = "Lv. " + userData.maxEnergyLv.ToString();
        mainMenuComponents.GetCoin4MaxEnergyTxt().text = CoinNeedToLevelUp(userData.maxEnergyLv).ToString();

        // Set enery refuel level texts
        mainMenuComponents.GetEnergyRefuelLvTxt().text = "Lv. " + userData.fuelPowerLv.ToString();
        mainMenuComponents.GetCoin4EnergyRefuelTxt().text = CoinNeedToLevelUp(userData.fuelPowerLv).ToString();

        // Set enery waste level texts
        mainMenuComponents.GetEnergyWasteLvTxt().text = "Lv. " + userData.energyDurabilityLv.ToString();
        mainMenuComponents.GetCoin4EnergyWasteTxt().text = CoinNeedToLevelUp(userData.energyDurabilityLv).ToString();
        
        mainMenuComponents.GetGameLevelTxt().text = "Level: " + userData.gameLevel;
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
        var settingsPanelComponents = guiMgr.AddPanel("SettingsPanel", ELayer.Top)
                                        .GetComponent<SettingsPanelComponents>();
        guiMgr.OnClick(settingsPanelComponents.GetExitGameBtn(), OnExitBtn);
        guiMgr.OnClick(settingsPanelComponents.GetXBtn(), OnSettingsXBtn);
        settingsPanelComponents.OnAddCoinBtn(OnAddCoinBtn);
        settingsPanelComponents.OnSetLevelBtn(OnSetLevelBtn);
        settingsPanelComponents.OnResetDataBtn(OnResetDataBtn);
        settingsPanelComponents.OnSaveGameBtn(OnSaveGameBtn);
    }

    private void OnSaveGameBtn()
    {
        gameManager.SaveGame();
    }

    private void OnResetDataBtn()
    {
        gameManager.ResetData();
        var mainMenuComponents = guiMgr.GetPanel("MainMenu").GetComponent<MainMenuComponents>();
        mainMenuComponents.GetCoinCntTxt().text = userData.coinCount.ToString();
        // Set max enery level texts
        mainMenuComponents.GetMaxEnergyLvTxt().text = "Lv. " + userData.maxEnergyLv.ToString();
        mainMenuComponents.GetCoin4MaxEnergyTxt().text = CoinNeedToLevelUp(userData.maxEnergyLv).ToString();

        // Set enery refuel level texts
        mainMenuComponents.GetEnergyRefuelLvTxt().text = "Lv. " + userData.fuelPowerLv.ToString();
        mainMenuComponents.GetCoin4EnergyRefuelTxt().text = CoinNeedToLevelUp(userData.fuelPowerLv).ToString();

        // Set enery waste level texts
        mainMenuComponents.GetEnergyWasteLvTxt().text = "Lv. " + userData.energyDurabilityLv.ToString();
        mainMenuComponents.GetCoin4EnergyWasteTxt().text = CoinNeedToLevelUp(userData.energyDurabilityLv).ToString();
        
        mainMenuComponents.GetGameLevelTxt().text = "Level: " + userData.gameLevel;
    }

    private void OnSetLevelBtn()
    {
        var value = guiMgr.GetPanel("SettingsPanel").GetComponent<SettingsPanelComponents>().GetSetLevelIFValue();
        int num;
        if (int.TryParse(value, out num)){
            userData.gameLevel = num;
        }
        guiMgr.GetPanel("MainMenu")
        .GetComponent<MainMenuComponents>()
        .GetGameLevelTxt().text = "Level: " + userData.gameLevel.ToString();
    }

    private void OnAddCoinBtn(){
        var value = guiMgr.GetPanel("SettingsPanel").GetComponent<SettingsPanelComponents>().GetAddCoinIFValue();
        int num;
        if (int.TryParse(value, out num)){
            userData.coinCount += num;
        }
        guiMgr.GetPanel("MainMenu")
        .GetComponent<MainMenuComponents>()
        .GetCoinCntTxt().text = userData.coinCount.ToString();
    }

    private void OnSettingsXBtn(){
        guiMgr.RemovePanel("SettingsPanel");
    }

    private void OnAddMaxEnergyBtn() {
        
        var coinNeed = CoinNeedToLevelUp(userData.maxEnergyLv);
        if (coinNeed > userData.coinCount) {
            return;
        }
        userData.maxEnergyLv += 1;
        userData.coinCount -= coinNeed;
        var mainMenuComponents = guiMgr.GetPanel("MainMenu").GetComponent<MainMenuComponents>();
        mainMenuComponents.GetMaxEnergyLvTxt().text = "Lv. " + userData.maxEnergyLv.ToString();
        mainMenuComponents.GetCoin4MaxEnergyTxt().text = CoinNeedToLevelUp(userData.maxEnergyLv).ToString();
        mainMenuComponents.GetCoinCntTxt().text = userData.coinCount.ToString();
    }

    private void OnAddEnergyRefuelBtn() {
        
        var coinNeed = CoinNeedToLevelUp(userData.fuelPowerLv);
        if (coinNeed > userData.coinCount) {
            return;
        }
        userData.fuelPowerLv += 1;
        userData.coinCount -= coinNeed;
        var mainMenuComponents = guiMgr.GetPanel("MainMenu").GetComponent<MainMenuComponents>();
        mainMenuComponents.GetEnergyRefuelLvTxt().text = "Lv. " + userData.fuelPowerLv.ToString();
        mainMenuComponents.GetCoin4EnergyRefuelTxt().text = CoinNeedToLevelUp(userData.fuelPowerLv).ToString();
        mainMenuComponents.GetCoinCntTxt().text = userData.coinCount.ToString();
    }

    private void OnAddEnergyWasteBtn() {
        
        var coinNeed = CoinNeedToLevelUp(userData.energyDurabilityLv);
        if (coinNeed > userData.coinCount) {
            return;
        }
        userData.energyDurabilityLv += 1;
        userData.coinCount -= coinNeed;
        var mainMenuComponents = guiMgr.GetPanel("MainMenu").GetComponent<MainMenuComponents>();
        mainMenuComponents.GetEnergyWasteLvTxt().text = "Lv. " + userData.energyDurabilityLv.ToString();
        mainMenuComponents.GetCoin4EnergyWasteTxt().text = CoinNeedToLevelUp(userData.energyDurabilityLv).ToString();
        mainMenuComponents.GetCoinCntTxt().text = userData.coinCount.ToString();
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
