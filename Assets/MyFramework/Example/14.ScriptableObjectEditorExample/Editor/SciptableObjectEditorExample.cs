using UnityEditor;
using UnityEngine;

namespace MyFramework{
    public class SciptableObjectEditorExample
    {
        #if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/14.ScriptableObject Editor Example", false, 14)]
        private static void MenuClicked(){
            var window = EditorWindow.GetWindow<ScriptableObjectEditor>();
            window.titleContent = new GUIContent("ScriptableObject Editor");
        }
        #endif
    }
}