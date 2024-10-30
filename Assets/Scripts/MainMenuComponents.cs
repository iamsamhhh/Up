using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuComponents : MonoBehaviour
{
    [SerializeField]
    Button addMaxEnergyBtn, addEnergyRefuelBtn, addEnergyWasteBtn,
        startGameBtn, settingsBtn;
    [SerializeField]
    TMP_Text maxEnergyLvTxt, energyRefuelLvTxt, energyWasteLvTxt,
        coinCntTxt, gameLevelTxt,
        coin4MaxEnergyTxt, coin4EnergyRefuelTxt, coin4EnergyWasteTxt;
    public Transform skinContent;
    public Button GetAddMaxEnergyBtn(){return addMaxEnergyBtn;}
    public Button GetAddEnergyRefuelBtn(){return addEnergyRefuelBtn;}
    public Button GetAddEnergyWasteBtn(){return addEnergyWasteBtn;}
    public Button GetSettingsBtn(){return settingsBtn;}
    public Button GetStartGameBtn(){return startGameBtn;}

    public TMP_Text GetMaxEnergyLvTxt(){return maxEnergyLvTxt;}
    public TMP_Text GetEnergyRefuelLvTxt() {return energyRefuelLvTxt;}
    public TMP_Text GetEnergyWasteLvTxt() { return energyWasteLvTxt;}
    public TMP_Text GetCoinCntTxt() { return coinCntTxt;}
    public TMP_Text GetCoin4MaxEnergyTxt() { return coin4MaxEnergyTxt;}
    public TMP_Text GetCoin4EnergyRefuelTxt() { return coin4EnergyRefuelTxt;}
    public TMP_Text GetCoin4EnergyWasteTxt() { return coin4EnergyWasteTxt;}
    public TMP_Text GetGameLevelTxt() { return gameLevelTxt;}
}
