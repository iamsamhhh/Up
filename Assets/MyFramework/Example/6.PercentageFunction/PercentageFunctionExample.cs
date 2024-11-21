using UnityEngine;

namespace MyFramework
{
    public partial class PercentageFunctionExample
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/6.Percentage function", false, 6)]
#endif
        private static void MenuClicked()
        {
            Debug.Log(MathUtil.Percent(50));
        }
    }
}