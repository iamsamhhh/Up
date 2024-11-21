using UnityEngine;

namespace MyFramework
{
    public class HideExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/9.Hide with MonoBehaviourSimplify", false, 9)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            var gameObj = new GameObject("Hide");
            gameObj.AddComponent<HideComponent>();
        }
#endif
    }
}