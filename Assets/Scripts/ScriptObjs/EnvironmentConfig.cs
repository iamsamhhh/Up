using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentConfig", menuName = "Configuration/Environment Config")]
public class EnvironmentConfig : ScriptableObject
{
    public EnvironmentMode mode;
    public UserData devData, testData, releaseData;
    public static EnvironmentConfig environment{
        get {
            if (!_environment)
                _environment = Resources.Load<EnvironmentConfig>("EnvironmentConfig");
            return _environment;
        }
    }
    private static EnvironmentConfig _environment;
#if UNITY_EDITOR
    [UnityEditor.MenuItem("UserData/ResetAllData")]
#endif
    public static void ResetAllData(){
        environment.devData.ResetData();
        environment.testData.ResetData();
        environment.releaseData.ResetData();
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("UserData/SaveAllData")]
#endif
    public static void SaveAllData(){
        environment.devData.Save();
        environment.testData.Save();
        environment.releaseData.Save();
    }

}

public enum EnvironmentMode{
    Developing,
    Testing,
    Release
}