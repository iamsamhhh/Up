using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace MyFramework{
    public partial class ReuseMenuItemExample
    {
#if UNITY_EDITOR
        [MenuItem("MyFramework/Example/3.Reuse MenuItem", false, 3)]
        private static void MenuClicked()
        {
            EditorUtil.CallMenuItem("MyFramework/Example/1.Copy 2 clipboard");
        }
#endif
    }
}
#endif