using UnityEngine;

namespace MyFramework
{
    public partial class GameObjectSimplify
    {
        public static void Show(GameObject go)
        {
            go.SetActive(true);
        }

        public static void Hide(GameObject go)
        {
            go.SetActive(false);
        }

        public static void Show(Transform transform)
        {
            transform.gameObject.SetActive(true);
        }

        public static void Hide(Transform transform)
        {
            transform.gameObject.SetActive(false);
        }
    }
}