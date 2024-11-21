#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MyFramework{
    public class Copy2ClipboardExample
    {
#if UNITY_EDITOR
        [MenuItem("MyFramework/Example/1.Copy 2 clipboard", false, 1)]
#endif
        private static void MenuClicked()
        {
            CommonUtil.CopyText2Clipboard("Copied text");
        }
    }
}