using System;
using UnityEngine;

namespace MyFramework{
    [CreateAssetMenu(fileName = "ScriptableObjectEditorConfig", menuName = "MyFramework/Configs/ScriptableObjectEditorConfig")]
    public class ScriptableObjectEditorConfig : ScriptableObject
    {
        public StringBoolDict showType;
    }
}

[Serializable]
public class StringBoolDict : SerializableDictionary<string, bool>{}