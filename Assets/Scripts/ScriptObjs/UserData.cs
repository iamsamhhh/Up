using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "Data/UserData")]
public class UserData : ScriptableObject
{
    public int coinCount, maxEnergyLv, fuelPowerLv, energyDurabilityLv, gameLevel, highestScore;
    public List<Skin> purchasedSkins;
    public Skin currentSkin;
    public static UserData defaultUserData{
        get {
            if (!_defaultUserData)
                _defaultUserData = Resources.Load<UserData>("UserDataForDev");
            return _defaultUserData;
        }
    }
    private static UserData _defaultUserData;
}
