using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace MyFramework {
    public partial class EditorUtil
    {
#if UNITY_EDITOR
    public static void CallMenuItem(string menuPath)
    {
        EditorApplication.ExecuteMenuItem(menuPath);
    }
    
    /// <summary>
    /// Open a folder using folder path
    /// </summary>
    /// <param name="folderPath">The path of the folder to be open</param>
    public static void OpenFolder(string folderPath)
    {
        Application.OpenURL("file:///" + folderPath);
    }

    /// <summary>
    /// Export a unity package
    /// </summary>
    /// <param name="assetPathName">The path of the asset need to export</param>
    /// <param name="fileName">The name of the exported package (Should end in .unitypackage)</param>
    public static void ExportPackage(string assetPathName, string fileName)
    {
        AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);
    }
#endif
    }
}

