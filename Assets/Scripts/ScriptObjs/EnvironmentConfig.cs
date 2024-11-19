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
}

public enum EnvironmentMode{
    Developing,
    Testing,
    Release
}