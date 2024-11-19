using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "Data/UserData")]
public class UserData : ScriptableObject
{
    public int coinCount, maxEnergyLv, fuelPowerLv, energyDurabilityLv, gameLevel, highestScore;
    public List<Skin> purchasedSkins;
    public Skin currentSkin;
    public static UserData userData{
        get {
            switch (EnvironmentConfig.environment.mode){
                case EnvironmentMode.Developing:
                    return EnvironmentConfig.environment.devData;
                case EnvironmentMode.Testing:
                    return EnvironmentConfig.environment.testData;
                case EnvironmentMode.Release:
                    return EnvironmentConfig.environment.releaseData;
                default:
                    return EnvironmentConfig.environment.devData;
            }
        }
    }
}
