using System;
using System.Collections.Generic;
using MyFramework;
using UnityEngine;

public class GameManager : MonoSingletonBaseAuto<GameManager>
{
    public UserData userData{
        get { return UserData.defaultUserData; }
    }

    public bool cameFromGame, gamePaused;
    [Obsolete("Use userData.currentSkin instead")]
    public Skin currentSkin;

    private void Awake() {
        if (SaveManager.LoadObject("UserData", userData)){
            Debug.Log("Success");
        }
        Dictionary<string, bool> skinDict = new Dictionary<string, bool>();
        
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
	    Application.targetFrameRate = 120;

        cameFromGame = false;
        gamePaused = false;
        
    }
    public void SaveGame(){
        SaveManager.SaveObject(userData, "UserData");
    }

    public void ResetData(){
        userData.coinCount = 0;
        userData.maxEnergyLv = 0;
        userData.fuelPowerLv = 0;
        userData.energyDurabilityLv = 0;
        userData.gameLevel = 1;
        userData.highestScore = 0;
        userData.currentSkin = SkinList.defaultSkinList.list[0];
        userData.purchasedSkins.Clear();

        SaveManager.SaveObject(userData, "UserData");
    }
}
