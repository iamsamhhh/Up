using System;
using System.Collections;
using System.Collections.Generic;
using MyFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingletonBaseAuto<GameManager>
{
    // private UserData _userData;
    public UserData userData{
        get { return UserData.defaultUserData; }
    }

    public bool cameFromGame, gamePaused;
    public Skin currentSkin;

    [Obsolete("Please use UserData instead")]
    public SkinList skinList;
    private void Awake() {
        userData.skinList = Resources.Load<SkinList>("DefaultList");
        Dictionary<string, bool> skinDict = new Dictionary<string, bool>();
        var currentSkinId = SaveManager.LoadString("CurrentSkinID");
        currentSkin = userData.skinList.skinList[0];
        if (SaveManager.LoadObject("SkinAvailibility", skinDict)){
            foreach (var skin in userData.skinList.skinList){
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
    }
    public void SaveGame(){
        userData.skinList = userData.skinList;

        SaveManager.SaveObject(userData, "UserData");
        Dictionary<string, bool> skinDict = new Dictionary<string, bool>();

        foreach (var skin in userData.skinList.skinList) {
            skinDict.Add(skin.id, skin.bought);
        }
        SaveManager.SaveObject(skinDict, "SkinAvailibility");
        SaveManager.Save(currentSkin.id, "CurrentSkinID");
    }

    public void ResetData(){
        userData.coinCount = 0;
        userData.maxEnergyLv = 0;
        userData.fuelPowerLv = 0;
        userData.energyDurabilityLv = 0;
        userData.gameLevel = 1;
        userData.highestScore = 0;
        foreach (var skin in userData.skinList.skinList){
            skin.bought = false;
        }
        userData.skinList.skinList[0].bought = true;
        currentSkin = userData.skinList.skinList[0];

        SaveManager.SaveObject(userData, "UserData");

        Dictionary<string, bool> skinDict = new Dictionary<string, bool>();
        foreach (var skin in userData.skinList.skinList) {
            skinDict.Add(skin.id, skin.bought);
        }
        SaveManager.SaveObject(skinDict, "SkinAvailibility");
        SaveManager.Save(currentSkin.id, "CurrentSkinID");
    }
}
