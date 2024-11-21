using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace MyFramework {

    public partial class Exporter
    {
#if UNITY_EDITOR
        [MenuItem("MyFramework/Framework/Util/Export UnityPackage %e", false, 1)]
#endif
        private static void MenuClicked()
        {
            var generatePackageName = GenerateUnityPackageName();
            
            EditorUtil.ExportPackage("Assets/MyFramework", generatePackageName + ".unitypackage");

            EditorUtil.OpenFolder(Path.Combine(Application.dataPath, "../"));
        }


        public static string GenerateUnityPackageName()
        {
            return "MyFramework_" + DateTime.Now.ToString("yyyyMMdd");
        }
    }
}