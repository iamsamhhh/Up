using System;
using System.Collections.Generic;
using MyFramework;
using UnityEngine;

public class GameManager : MonoSingletonBaseAuto<GameManager>
{
    public UserData userData{
        get { return UserData.userData; }
    }

    public bool cameFromGame, gamePaused;

    private void Awake() {
        UserData.ResetUserData();
        if (!userData.currentSkin){
            userData.currentSkin = SkinList.defaultSkinList.list[0];
        }
        if (userData.purchasedSkins.Count == 0){
            userData.purchasedSkins.Add(userData.currentSkin);
        }
            

        QualitySettings.vSyncCount = 0;  // VSync must be disabled
	    Application.targetFrameRate = 120;

        cameFromGame = false;
        gamePaused = false;
        
    }
    public void SaveGame(){
        SaveManager.SaveObject(userData, userData.fileName);
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
        userData.purchasedSkins.Add(userData.currentSkin);

        SaveManager.SaveObject(userData, userData.fileName);
    }
}
