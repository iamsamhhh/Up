using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFramework;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    private GUIMgr guiMgr;
    private GameManager gameManager;
    private void Awake() {
        guiMgr = GUIMgr.instance;
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
        foreach (var skin in gameManager.skinList.skinList){
            var btnScript = Instantiate(Resources.Load<GameObject>("SkinBtn"), mainMenuComponents.skinContent)
                                .GetComponent<SkinBtn>();
            btnScript.skin = skin;
            guiMgr.OnClick(btnScript.btn, btnScript.OnClick);
            btnScript.rawImage.texture = skin.thumbnail;
            btnScript.costText.text = skin.cost.ToString();
        }
        mainMenuComponents.GetCoinCntTxt().text = GameManager.instance.coinCount.ToString();
        guiMgr.OnClick(mainMenuComponents.GetSettingsBtn(), OnSettingsBtn);
        guiMgr.OnClick(mainMenuComponents.GetAddMaxEnergyBtn(), OnAddMaxEnergyBtn);
        guiMgr.OnClick(mainMenuComponents.GetAddEnergyRefuelBtn(), OnAddEnergyRefuelBtn);
        guiMgr.OnClick(mainMenuComponents.GetAddEnergyWasteBtn(), OnAddEnergyWasteBtn);

        // Set max enery level texts
        mainMenuComponents.GetMaxEnergyLvTxt().text = "Lv. " + gameManager.maxEnergyLv.ToString();
        mainMenuComponents.GetCoin4MaxEnergyTxt().text = CoinNeedToLevelUp(gameManager.maxEnergyLv).ToString();

        // Set enery refuel level texts
        mainMenuComponents.GetEnergyRefuelLvTxt().text = "Lv. " + gameManager.fuelPowerLv.ToString();
        mainMenuComponents.GetCoin4EnergyRefuelTxt().text = CoinNeedToLevelUp(gameManager.fuelPowerLv).ToString();

        // Set enery waste level texts
        mainMenuComponents.GetEnergyWasteLvTxt().text = "Lv. " + gameManager.energyDurabilityLv.ToString();
        mainMenuComponents.GetCoin4EnergyWasteTxt().text = CoinNeedToLevelUp(gameManager.energyDurabilityLv).ToString();
        
        mainMenuComponents.GetGameLevelTxt().text = "Level: " + gameManager.gameLevel;
    }

    private void OnExitBtn(){
        gameManager.SaveGame();
        Application.Quit();
    }

    private void OnStartGameBtn(){
        guiMgr.RemoveAllPanel();
        gameManager.SaveGame();
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
        mainMenuComponents.GetCoinCntTxt().text = gameManager.coinCount.ToString();
        // Set max enery level texts
        mainMenuComponents.GetMaxEnergyLvTxt().text = "Lv. " + gameManager.maxEnergyLv.ToString();
        mainMenuComponents.GetCoin4MaxEnergyTxt().text = CoinNeedToLevelUp(gameManager.maxEnergyLv).ToString();

        // Set enery refuel level texts
        mainMenuComponents.GetEnergyRefuelLvTxt().text = "Lv. " + gameManager.fuelPowerLv.ToString();
        mainMenuComponents.GetCoin4EnergyRefuelTxt().text = CoinNeedToLevelUp(gameManager.fuelPowerLv).ToString();

        // Set enery waste level texts
        mainMenuComponents.GetEnergyWasteLvTxt().text = "Lv. " + gameManager.energyDurabilityLv.ToString();
        mainMenuComponents.GetCoin4EnergyWasteTxt().text = CoinNeedToLevelUp(gameManager.energyDurabilityLv).ToString();
        
        mainMenuComponents.GetGameLevelTxt().text = "Level: " + gameManager.gameLevel;
    }

    private void OnSetLevelBtn()
    {
        var value = guiMgr.GetPanel("SettingsPanel").GetComponent<SettingsPanelComponents>().GetSetLevelIFValue();
        int num;
        if (int.TryParse(value, out num)){
            gameManager.gameLevel = num;
        }
        guiMgr.GetPanel("MainMenu")
        .GetComponent<MainMenuComponents>()
        .GetGameLevelTxt().text = "Level: " + gameManager.gameLevel.ToString();
    }

    private void OnAddCoinBtn(){
        var value = guiMgr.GetPanel("SettingsPanel").GetComponent<SettingsPanelComponents>().GetAddCoinIFValue();
        int num;
        if (int.TryParse(value, out num)){
            gameManager.coinCount += num;
        }
        guiMgr.GetPanel("MainMenu")
        .GetComponent<MainMenuComponents>()
        .GetCoinCntTxt().text = gameManager.coinCount.ToString();
    }

    private void OnSettingsXBtn(){
        guiMgr.RemovePanel("SettingsPanel");
    }

    private void OnAddMaxEnergyBtn() {
        
        var coinNeed = CoinNeedToLevelUp(gameManager.maxEnergyLv);
        if (coinNeed > gameManager.coinCount) {
            return;
        }
        gameManager.maxEnergyLv += 1;
        gameManager.coinCount -= coinNeed;
        var mainMenuComponents = guiMgr.GetPanel("MainMenu").GetComponent<MainMenuComponents>();
        mainMenuComponents.GetMaxEnergyLvTxt().text = "Lv. " + gameManager.maxEnergyLv.ToString();
        mainMenuComponents.GetCoin4MaxEnergyTxt().text = CoinNeedToLevelUp(gameManager.maxEnergyLv).ToString();
        mainMenuComponents.GetCoinCntTxt().text = gameManager.coinCount.ToString();
    }

    private void OnAddEnergyRefuelBtn() {
        
        var coinNeed = CoinNeedToLevelUp(gameManager.fuelPowerLv);
        if (coinNeed > gameManager.coinCount) {
            return;
        }
        gameManager.fuelPowerLv += 1;
        gameManager.coinCount -= coinNeed;
        var mainMenuComponents = guiMgr.GetPanel("MainMenu").GetComponent<MainMenuComponents>();
        mainMenuComponents.GetEnergyRefuelLvTxt().text = "Lv. " + gameManager.fuelPowerLv.ToString();
        mainMenuComponents.GetCoin4EnergyRefuelTxt().text = CoinNeedToLevelUp(gameManager.fuelPowerLv).ToString();
        mainMenuComponents.GetCoinCntTxt().text = gameManager.coinCount.ToString();
    }

    private void OnAddEnergyWasteBtn() {
        
        var coinNeed = CoinNeedToLevelUp(gameManager.energyDurabilityLv);
        if (coinNeed > gameManager.coinCount) {
            return;
        }
        gameManager.energyDurabilityLv += 1;
        gameManager.coinCount -= coinNeed;
        var mainMenuComponents = guiMgr.GetPanel("MainMenu").GetComponent<MainMenuComponents>();
        mainMenuComponents.GetEnergyWasteLvTxt().text = "Lv. " + gameManager.energyDurabilityLv.ToString();
        mainMenuComponents.GetCoin4EnergyWasteTxt().text = CoinNeedToLevelUp(gameManager.energyDurabilityLv).ToString();
        mainMenuComponents.GetCoinCntTxt().text = gameManager.coinCount.ToString();
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
