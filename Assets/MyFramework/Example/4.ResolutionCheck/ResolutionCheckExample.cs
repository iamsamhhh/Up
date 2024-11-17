using UnityEngine;

namespace MyFramework
{
    public class ResolutionCheckExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/4.Resolution check", false, 4)]
#endif
        private static void MenuClicked()
        {
            Debug.Log(ResolutionCheck.IsPadResolution() ? "是 Pad 分辨率" : "不是 Pad 分辨率");
            Debug.Log(ResolutionCheck.IsPhoneResolution() ? "是 Phone 分辨率" : "不是 Phone 分辨率");
            Debug.Log(ResolutionCheck.IsiPhoneXResolution() ? "是 iPhone X 分辨率" : "不是 iPhone X 分辨率");
        }
    }
}