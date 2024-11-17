using UnityEngine;

namespace MyFramework
{
    public class TransformSimplifyExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/5.Transform simplify", false, 5)]
#endif
        private static void MenuClicked()
        {
            var transform = new GameObject("transform").transform;

            TransformSimplify.SetLocalPosX(transform, 5.0f);
            TransformSimplify.SetLocalPosY(transform, 5.0f);
            TransformSimplify.SetLocalPosZ(transform, 5.0f);
            TransformSimplify.Identity(transform);
            
            var childTrans = new GameObject("Child").transform;

            TransformSimplify.AddChild(transform,childTrans);
        }
    }
}