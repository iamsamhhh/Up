using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "Data/UserData")]
public class UserData : ScriptableObject
{
    public int coinCount, maxEnergyLv, fuelPowerLv, energyDurabilityLv, gameLevel, highestScore;
    public SkinList skinList;
    public static UserData defaultUserData{
        get {
            if (!_defaultUserData)
                _defaultUserData = Resources.Load<UserData>("UserDataForDev");
            return _defaultUserData;
        }
    }
    private static UserData _defaultUserData;
}
