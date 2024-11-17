using UnityEngine;

namespace MyFramework{
    public partial class RandomValueFunctionExample
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/8.Percentage function", false, 8)]
#endif
        private static void MenuClicked()
        {
            Debug.Log(MathUtil.GetRandomValueFrom(1, 2, 3));
            Debug.Log(MathUtil.GetRandomValueFrom("asdasd", "123123"));
            Debug.Log(MathUtil.GetRandomValueFrom(0.1f, 0.2f));
        }
    }
}