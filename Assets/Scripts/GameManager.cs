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
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
	    Application.targetFrameRate = 120;
        // SaveMgr.instance.isBuild = true;
        cameFromGame = false;
        gamePaused = false;
        currentSkin = skinList.skinList[0];
        // coinCount = PlayerPrefs.GetInt("CoinCount", 0);

        // maxEnergyLv = PlayerPrefs.GetInt("MaxEnergyLv", 0);

        // energyRefuelLv = PlayerPrefs.GetInt("EnergyRefuelLv", 0);

        // energyWasteLv = PlayerPrefs.GetInt("EnergyWasteLv", 0);

        // gameLevel = PlayerPrefs.GetInt("GameLevel", 1);
        
        coinCount = SaveMgr.instance.LoadInt("GoldCount");
        Debug.Log("Getting Coin count");
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
    }
}
