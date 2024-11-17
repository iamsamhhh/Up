using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEditor.Search;
using System.Linq;

namespace MyFramework{
    public class ScriptableObjectEditor : EditorWindow
    {
        private Dictionary<Type, List<ScriptableObject>> scriptableObjectDict = new Dictionary<Type, List<ScriptableObject>>();
        private Dictionary<ScriptableObject, string> scriptableObjectPathDict = new Dictionary<ScriptableObject, string>();
        private Dictionary<Type, bool> typeFoldedDict = new Dictionary<Type, bool>();
        private ScriptableObject currenScriptableObject;
        private Vector2 scrollPosition;
        private bool showUnityScriptableObject = false;
        private bool showFullTypeName = false;
        private float listWidthPercentage = 0.3f;

        [MenuItem("MyFramework/Framework/Util/ScrptableObject Editor")]
        public static void MenuClicked(){
            var window = GetWindow<ScriptableObjectEditor>();
            window.titleContent = new GUIContent("ScriptableObject Editor");
            window.showUnityScriptableObject = false;
            window.showFullTypeName = false;
            window.RefreshList();
            window.currenScriptableObject = null;
        }
        

        private void OnGUI(){
            EditorGUILayout.BeginVertical();
            
            if (GUILayout.Button("Refresh list"))
                RefreshList();
            showUnityScriptableObject = GUILayout.Toggle(showUnityScriptableObject, new GUIContent("Show Unity ScriptableObjects"));
            showFullTypeName = GUILayout.Toggle(showFullTypeName, new GUIContent("Show full type name"));
            listWidthPercentage = EditorGUILayout.Slider(listWidthPercentage, 0, 1);
            EditorGUILayout.BeginHorizontal();
            DrawList();
            DrawInspector();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
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
                if (!typeFoldedDict.ContainsKey(scriptableObject.GetType()))
                    typeFoldedDict.Add(scriptableObject.GetType(), false);
            }
        }

        private void DrawList(){
            EditorGUILayout.BeginVertical(EditorStyles.objectField, GUILayout.Width(position.width * listWidthPercentage));

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            foreach (var record in scriptableObjectDict){
                bool needToShow = true;
                if (!(record.Key.ToString().Length < 5))
                    needToShow = record.Key.ToString().Substring(0, 5) != "Unity" || showUnityScriptableObject;
                
                if (needToShow){
                    if (showFullTypeName)
                        typeFoldedDict[record.Key] = EditorGUILayout.Foldout(typeFoldedDict[record.Key], record.Key.ToString());
                    else
                        typeFoldedDict[record.Key] = EditorGUILayout.Foldout(typeFoldedDict[record.Key], record.Key.ToString().Split('.').Last());
                    if (!typeFoldedDict[record.Key]) continue;
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

            EditorGUILayout.EndVertical();
        }

        private void DrawInspector(){
            EditorGUILayout.BeginVertical(GUILayout.Width(position.width * (1-listWidthPercentage)));

            if(!currenScriptableObject) goto exit;

            var editor = Editor.CreateEditor(currenScriptableObject);
            editor.OnInspectorGUI();

            if (GUILayout.Button("Ping")){
                SearchUtils.PingAsset(scriptableObjectPathDict[currenScriptableObject]);
            }


            exit:
            EditorGUILayout.EndVertical();
        }
    }
}