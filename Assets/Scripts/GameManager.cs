using System;
using System.Collections;
using System.Collections.Generic;
using MyFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingletonBaseAuto<GameManager>
{
    [Obsolete("Please use GameManager.instance.userData instead")]
    public int coinCount, maxEnergyLv, fuelPowerLv, energyDurabilityLv, gameLevel, highestScore;

    // private UserData _userData;
    public UserData userData;

    public bool cameFromGame, gamePaused;
    public Skin currentSkin;
    public SkinList skinList;
    private void Awake() {
        userData = Resources.Load<UserData>("UserDataForDev");
        skinList = Resources.Load<SkinList>("DefaultList");
        Dictionary<string, bool> skinDict = new Dictionary<string, bool>();
        var currentSkinId = SaveManager.LoadString("CurrentSkinID");
        currentSkin = skinList.skinList[0];
        if (SaveManager.LoadObject("SkinAvailibility", skinDict)){
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

        if (SaveManager.LoadObject("UserData", userData)){
            Debug.Log("Success");
        }
        coinCount = SaveManager.LoadInt("GoldCount");
        // Debug.Log("Getting Coin count");
        if (coinCount == 0) {
            SaveManager.Save(0, "GoldCount");
        }
        maxEnergyLv = SaveManager.LoadInt("MaxEnergyLv");
        if (maxEnergyLv == 0) {
            SaveManager.Save(0, "MaxEnergyLv");
        }
        fuelPowerLv = SaveManager.LoadInt("EnergyRefuelLv");
        if (fuelPowerLv == 0) {
            SaveManager.Save(0, "EnergyRefuelLv");
        }
        energyDurabilityLv = SaveManager.LoadInt("EnergyWasteLv");
        if (energyDurabilityLv == 0) {
            SaveManager.Save(0, "EnergyWasteLv");
        }
        gameLevel = SaveManager.LoadInt("GameLevel");
        if (gameLevel == 0) {
            gameLevel = 1;
            SaveManager.Save(gameLevel, "GameLevel");
        }
        highestScore = SaveManager.LoadInt("HighestScore");
        if (highestScore == 0) {
            SaveManager.Save(0, "HighestScore");
        }
    }
    public void SaveGame(){
        userData.coinCount = coinCount;
        userData.maxEnergyLv = maxEnergyLv;
        userData.gameLevel = gameLevel;
        userData.highestScore = highestScore;
        userData.fuelPowerLv = fuelPowerLv;
        userData.energyDurabilityLv = energyDurabilityLv;
        userData.skinList = skinList;

        SaveManager.SaveObject(userData, "UserData");
        SaveManager.Save(coinCount, "GoldCount");
        SaveManager.Save(maxEnergyLv, "MaxEnergyLv");
        SaveManager.Save(fuelPowerLv, "EnergyRefuelLv");
        SaveManager.Save(energyDurabilityLv, "EnergyWasteLv");
        SaveManager.Save(gameLevel, "GameLevel");
        SaveManager.Save(highestScore, "HighestScore");
        Dictionary<string, bool> skinDict = new Dictionary<string, bool>();

        foreach (var skin in skinList.skinList) {
            skinDict.Add(skin.id, skin.bought);
        }
        SaveManager.SaveObject(skinDict, "SkinAvailibility");
        SaveManager.Save(currentSkin.id, "CurrentSkinID");
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

        userData.coinCount = coinCount;
        userData.maxEnergyLv = maxEnergyLv;
        userData.gameLevel = gameLevel;
        userData.highestScore = highestScore;
        userData.fuelPowerLv = fuelPowerLv;
        userData.energyDurabilityLv = energyDurabilityLv;
        userData.skinList = skinList;

        SaveManager.SaveObject(userData, "UserData");

        SaveManager.Save(coinCount, "GoldCount");
        SaveManager.Save(maxEnergyLv, "MaxEnergyLv");
        SaveManager.Save(fuelPowerLv, "EnergyRefuelLv");
        SaveManager.Save(energyDurabilityLv, "EnergyWasteLv");
        SaveManager.Save(gameLevel, "GameLevel");
        SaveManager.Save(highestScore, "HighestScore");
        Dictionary<string, bool> skinDict = new Dictionary<string, bool>();
        foreach (var skin in skinList.skinList) {
            skinDict.Add(skin.id, skin.bought);
        }
        SaveManager.SaveObject(skinDict, "SkinAvailibility");
        SaveManager.Save(currentSkin.id, "CurrentSkinID");
    }
}
