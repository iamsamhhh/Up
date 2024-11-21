using UnityEngine;

namespace MyFramework
{
    public class GameObjectSimplifyExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/7.GameObejct simplify", false, 7)]
#endif
        private static void MenuClicked()
        {
            var gameObject = new GameObject();

            GameObjectSimplify.Hide(gameObject);
            GameObjectSimplify.Show(gameObject.transform);
        }
    }
}