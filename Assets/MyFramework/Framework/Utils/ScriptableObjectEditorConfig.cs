using System;
using UnityEngine;

namespace MyFramework{
    [CreateAssetMenu(fileName = "ScriptableObjectEditorConfig", menuName = "MyFramework/Configs/ScriptableObjectEditorConfig")]
    public class ScriptableObjectEditorConfig : ScriptableObject
    {
        

        public StringBoolDict showType = new StringBoolDict();
    }
}

[Serializable]
public class StringBoolDict : SerializableDictionary<string, bool>{}