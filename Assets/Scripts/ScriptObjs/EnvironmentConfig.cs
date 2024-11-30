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
    private static void ResetAllData(){
        environment.devData.ResetData();
        environment.testData.ResetData();
        environment.releaseData.ResetData();
    }
#endif
}

public enum EnvironmentMode{
    Developing,
    Testing,
    Release
}