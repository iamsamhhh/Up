using System.Collections;
using System.Collections.Generic;
using SFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingletonBaseAuto<GameManager>
{
    public int coinCount, maxEnergyLv, energyRefuelLv, energyWasteLv, gameLevel, inGameCoinCnt;

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
        // SaveMgr.instance.isBuild = true;
        cameFromGame = false;
        gamePaused = false;

        // coinCount = PlayerPrefs.GetInt("CoinCount", 0);

        // maxEnergyLv = PlayerPrefs.GetInt("MaxEnergyLv", 0);

        // energyRefuelLv = PlayerPrefs.GetInt("EnergyRefuelLv", 0);

        // energyWasteLv = PlayerPrefs.GetInt("EnergyWasteLv", 0);

        // gameLevel = PlayerPrefs.GetInt("GameLevel", 1);
        
        coinCount = SaveMgr.instance.LoadInt("GoldCount");
        // Debug.Log("Getting Coin count");
        if (coinCount == 0) {
            SaveMgr.instance.Save(0, "GoldCount");
        }
        maxEnergyLv = SaveMgr.instance.LoadInt("MaxEnergyLv");
        if (maxEnergyLv == 0) {
            SaveMgr.instance.Save(0, "MaxEnergyLv");
        }
        energyRefuelLv = SaveMgr.instance.LoadInt("EnergyRefuelLv");
        if (energyRefuelLv == 0) {
            SaveMgr.instance.Save(0, "EnergyRefuelLv");
        }
        energyWasteLv = SaveMgr.instance.LoadInt("EnergyWasteLv");
        if (energyWasteLv == 0) {
            SaveMgr.instance.Save(0, "EnergyWasteLv");
        }
        gameLevel = SaveMgr.instance.LoadInt("GameLevel");
        if (gameLevel == 0) {
            gameLevel = 1;
            SaveMgr.instance.Save(gameLevel, "GameLevel");
        }
    }
    public void SaveGame(){
        SaveMgr.instance.Save(coinCount, "GoldCount");
        SaveMgr.instance.Save(maxEnergyLv, "MaxEnergyLv");
        SaveMgr.instance.Save(energyRefuelLv, "EnergyRefuelLv");
        SaveMgr.instance.Save(energyWasteLv, "EnergyWasteLv");
        SaveMgr.instance.Save(gameLevel, "GameLevel");
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
        energyRefuelLv = 0;
        energyWasteLv = 0;
        gameLevel = 1;
        foreach (var skin in skinList.skinList){
            skin.bought = false;
        }
        skinList.skinList[0].bought = true;
        currentSkin = skinList.skinList[0];
        // PlayerPrefs.SetInt("CoinCount", coinCount);
        // PlayerPrefs.SetInt("MaxEnergyLv", maxEnergyLv);
        // PlayerPrefs.SetInt("EnergyRefuelLv", energyRefuelLv);
        // PlayerPrefs.SetInt("EnergyWasteLv", energyWasteLv);
        // PlayerPrefs.SetInt("GameLevel", gameLevel);
        // PlayerPrefs.Save();
        SaveMgr.instance.Save(coinCount, "GoldCount");
        SaveMgr.instance.Save(maxEnergyLv, "MaxEnergyLv");
        SaveMgr.instance.Save(energyRefuelLv, "EnergyRefuelLv");
        SaveMgr.instance.Save(energyWasteLv, "EnergyWasteLv");
        SaveMgr.instance.Save(gameLevel, "GameLevel");
        Dictionary<string, bool> skinDict = new Dictionary<string, bool>();
        foreach (var skin in skinList.skinList) {
            skinDict.Add(skin.id, skin.bought);
        }
        SaveMgr.instance.Save(skinDict, "SkinAvailibility");
        SaveMgr.instance.Save(currentSkin.id, "CurrentSkinID");
    }
}
