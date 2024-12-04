using System.Collections.Generic;
using UnityEngine;
using MyFramework;

[CreateAssetMenu(fileName = "UserData", menuName = "Data/UserData")]
public class UserData : ScriptableObject
{
    public int coinCount, maxEnergyLv, fuelPowerLv, energyDurabilityLv, gameLevel, highestScore;
    public List<Skin> purchasedSkins;
    public Skin currentSkin;
    public string fileName;
    public static UserData userData{
        get {
            if (!_userData){
                switch (EnvironmentConfig.environment.mode) {
                    case EnvironmentMode.Developing:
                        if (SaveManager.LoadObject(EnvironmentConfig.environment.devData.fileName, EnvironmentConfig.environment.devData))
                            Debug.Log("User data loaded");
                        _userData = EnvironmentConfig.environment.devData;
                        break;
                    case EnvironmentMode.Testing:
                        if (SaveManager.LoadObject(EnvironmentConfig.environment.testData.fileName, EnvironmentConfig.environment.testData))
                            Debug.Log("User data loaded");
                        _userData = EnvironmentConfig.environment.testData;
                        break;
                    case EnvironmentMode.Release:
                        if (SaveManager.LoadObject(EnvironmentConfig.environment.releaseData.fileName, EnvironmentConfig.environment.releaseData))
                            Debug.Log("User data loaded");
                        _userData = EnvironmentConfig.environment.releaseData;
                        break;
                }
            }
            return _userData;
        }
    }

    private static UserData _userData;

    public static void ResetUserDataReference(){
        _userData = null;
    }

    public void ResetData(){
        coinCount = 0;
        maxEnergyLv = 0;
        fuelPowerLv = 0;
        energyDurabilityLv = 0;
        gameLevel = 1;
        highestScore = 0;
        currentSkin = SkinList.defaultSkinList.list[0];
        purchasedSkins.Clear();
        purchasedSkins.Add(userData.currentSkin);

        // SaveManager.SaveObject(this, fileName);
    }

    public void Save(){
        SaveManager.SaveObject(this, fileName);
    }
}
