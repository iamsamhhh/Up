#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.IO;

namespace MyFramework{
    public class OpenFolderExample
    {
#if UNITY_EDITOR
        [MenuItem("MyFramework/Example/2.Open folder", false, 2)]

        private static void MenuClicked(){
            EditorUtil.OpenFolder(Path.Combine(Application.dataPath, "../"));
        }
#endif
    }

}