using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin list", menuName = "Skin/Create skin list")]
public class SkinList : ScriptableObject
{
    public List<Skin> list = new List<Skin>();
    public static SkinList defaultSkinList {
        get {
            if (!_defaultSkinList)
                _defaultSkinList = Resources.Load<SkinList>("DefaultList");
            return _defaultSkinList;
        }
    }
    private static SkinList _defaultSkinList;
}
