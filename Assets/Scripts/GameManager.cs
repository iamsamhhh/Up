using System.Collections;
using System.Collections.Generic;
using SFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingletonBaseAuto<GameManager>
{
    public int coinCount, maxEnergyLv, fuelPowerLv, energyDurabilityLv, gameLevel, highestScore;

    public bool cameFromGame, gamePaused;
    public Skin currentSkin;
    public SkinList skinList;
    private void Awake() {
        skinList = Resources.Load<SkinList>("DefaultList");
        Dictionary<string, bool> skinDict = new Dictionary<string, bool>();
        var currentSkinId = SaveMgr.instance.LoadString("CurrentSkinID");
        currentSkin = skinList.skinList[0];
        if (SaveMgr.instance.LoadObject("SkinAvailibility", skinDict)){
            foreach (var skin in skinList.skinList){
                if (skinDict.ContainsKey(skin.id))
                    skin.bought = skinDict[skin.id];
                if (skin.id == currentSkinId){
                    if (skin.bought)
                        currentSkin = skin;
                }
            }
        }
        
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
	    Application.targetFrameRate = 120;

        cameFromGame = false;
        gamePaused = false;

        coinCount = SaveMgr.instance.LoadInt("GoldCount");
        // Debug.Log("Getting Coin count");
        if (coinCount == 0) {
            SaveMgr.instance.Save(0, "GoldCount");
        }
        maxEnergyLv = SaveMgr.instance.LoadInt("MaxEnergyLv");
        if (maxEnergyLv == 0) {
            SaveMgr.instance.Save(0, "MaxEnergyLv");
        }
        fuelPowerLv = SaveMgr.instance.LoadInt("EnergyRefuelLv");
        if (fuelPowerLv == 0) {
            SaveMgr.instance.Save(0, "EnergyRefuelLv");
        }
        energyDurabilityLv = SaveMgr.instance.LoadInt("EnergyWasteLv");
        if (energyDurabilityLv == 0) {
            SaveMgr.instance.Save(0, "EnergyWasteLv");
        }
        gameLevel = SaveMgr.instance.LoadInt("GameLevel");
        if (gameLevel == 0) {
            gameLevel = 1;
            SaveMgr.instance.Save(gameLevel, "GameLevel");
        }
        highestScore = SaveMgr.instance.LoadInt("HighestScore");
        if (highestScore == 0) {
            SaveMgr.instance.Save(0, "HighestScore");
        }
    }
    public void SaveGame(){
        SaveMgr.instance.Save(coinCount, "GoldCount");
        SaveMgr.instance.Save(maxEnergyLv, "MaxEnergyLv");
        SaveMgr.instance.Save(fuelPowerLv, "EnergyRefuelLv");
        SaveMgr.instance.Save(energyDurabilityLv, "EnergyWasteLv");
        SaveMgr.instance.Save(gameLevel, "GameLevel");
        SaveMgr.instance.Save(highestScore, "HighestScore");
        Dictionary<string, bool> skinDict = new Dictionary<string, bool>();

        foreach (var skin in skinList.skinList) {
            skinDict.Add(skin.id, skin.bought);
        }
        SaveMgr.instance.Save(skinDict, "SkinAvailibility");
        SaveMgr.instance.Save(currentSkin.id, "CurrentSkinID");
        // PlayerPrefs.SetInt("CoinCount", coinCount);
        // PlayerPrefs.SetInt("MaxEnergyLv", maxEnergyLv);
        // PlayerPrefs.SetInt("EnergyRefuelLv", energyRefuelLv);
        // PlayerPrefs.SetInt("EnergyWasteLv", energyWasteLv);
        // PlayerPrefs.SetInt("GameLevel", gameLevel);
        // PlayerPrefs.Save();

    }

    public void ResetData(){
        coinCount = 0;
        maxEnergyLv = 0;
        fuelPowerLv = 0;
        energyDurabilityLv = 0;
        gameLevel = 1;
        highestScore = 0;
        foreach (var skin in skinList.skinList){
            skin.bought = false;
        }
        skinList.skinList[0].bought = true;
        currentSkin = skinList.skinList[0];
        SaveMgr.instance.Save(coinCount, "GoldCount");
        SaveMgr.instance.Save(maxEnergyLv, "MaxEnergyLv");
        SaveMgr.instance.Save(fuelPowerLv, "EnergyRefuelLv");
        SaveMgr.instance.Save(energyDurabilityLv, "EnergyWasteLv");
        SaveMgr.instance.Save(gameLevel, "GameLevel");
        SaveMgr.instance.Save(highestScore, "HighestScore");
        Dictionary<string, bool> skinDict = new Dictionary<string, bool>();
        foreach (var skin in skinList.skinList) {
            skinDict.Add(skin.id, skin.bought);
        }
        SaveMgr.instance.Save(skinDict, "SkinAvailibility");
        SaveMgr.instance.Save(currentSkin.id, "CurrentSkinID");
    }
}
