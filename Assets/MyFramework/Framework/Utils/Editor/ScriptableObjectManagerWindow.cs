using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEditor.Search;

namespace MyFramework{
    public class ScriptableObjectEditor : EditorWindow
    {
        private static ScriptableObjectEditor window;
        private Dictionary<Type, List<ScriptableObject>> scriptableObjectDict = new Dictionary<Type, List<ScriptableObject>>();
        private Dictionary<ScriptableObject, string> scriptableObjectPathDict = new Dictionary<ScriptableObject, string>();
        private ScriptableObject currenScriptableObject;
        private Vector2 scrollPosition;
        private bool showUnityScriptableObject = false;
        private const float LIST_WIDTH_PERCENTAGE = 0.3f;

        [MenuItem("MyFramework/Framework/Util/ScrptableObject Editor")]
        public static void MenuClicked(){
            window = GetWindow<ScriptableObjectEditor>();
            window.titleContent = new GUIContent("ScriptableObject Editor");
        }

        private void OnGUI(){
            DrawList();
            DrawInspector();
        }

        private void OnEnable() {
            RefreshList();
        }

        private void RefreshList(){
            scriptableObjectDict.Clear();
            scriptableObjectPathDict.Clear();
            var assetGUIDs = AssetDatabase.FindAssets("t: ScriptableObject");
            foreach (var guid in assetGUIDs){
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                scriptableObjectPathDict.Add(scriptableObject, path);
                if (scriptableObjectDict.ContainsKey(scriptableObject.GetType())){
                    scriptableObjectDict[scriptableObject.GetType()].Add(scriptableObject);
                }
                else{
                    scriptableObjectDict.Add(scriptableObject.GetType(), new List<ScriptableObject>());
                    scriptableObjectDict[scriptableObject.GetType()].Add(scriptableObject);
                }
            }
        }

        private void DrawList(){
            GUILayout.BeginArea(
                new Rect(0, 0, window.position.width * LIST_WIDTH_PERCENTAGE, window.position.height), 
                new GUIStyle("FrameBox")
            );
            showUnityScriptableObject = GUILayout.Toggle(showUnityScriptableObject, new GUIContent("Show Unity ScriptableObjects"));

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            foreach (var record in scriptableObjectDict){
                if (showUnityScriptableObject){
                    EditorGUILayout.LabelField(record.Key.ToString(), EditorStyles.boldLabel);
                    foreach (var scriptableObject in record.Value){
                        if(!scriptableObject)
                            continue;
                        if (GUILayout.Button(scriptableObject.name)){
                            currenScriptableObject = scriptableObject;
                        }
                    }
                }
                else{
                    if (record.Key.ToString().Substring(0, 5) == "Unity") continue;
                    EditorGUILayout.LabelField(record.Key.ToString(), EditorStyles.boldLabel);
                    foreach (var scriptableObject in record.Value){
                        if(!scriptableObject)
                            continue;
                        if (GUILayout.Button(scriptableObject.name)){
                            currenScriptableObject = scriptableObject;
                        }
                    }
                }
            }

            EditorGUILayout.EndScrollView();

            GUILayout.EndArea();
        }

        private void DrawInspector(){
            GUILayout.BeginArea (
                new Rect(window.position.width * LIST_WIDTH_PERCENTAGE, y: 0, window.position.width * (1 - LIST_WIDTH_PERCENTAGE), window.position.height), 
                new GUIStyle("FrameBox")
            );

            if(!currenScriptableObject) goto exit;

            var editor = Editor.CreateEditor(currenScriptableObject);
            editor.OnInspectorGUI();

            if (GUILayout.Button("Ping")){
                SearchUtils.PingAsset(scriptableObjectPathDict[currenScriptableObject]);
            }


            exit:
            GUILayout.EndArea();
        }
    }
}