using UnityEngine;

namespace MyFramework
{
    public class DelayWithCoroutine : MonoBehaviourSimplify
    {
        private void Start()
        {
            Delay(5.0f, () =>
            {
                UnityEditor.EditorApplication.isPlaying = false;
            });
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/10.Delay example", false, 10)]
        private static void MenuClickd()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("DelayWithCoroutine")
                .AddComponent<DelayWithCoroutine>();
        }
#endif
    }
}