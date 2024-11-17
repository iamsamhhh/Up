using UnityEngine;

namespace MyFramework
{
    public partial class CommonUtil
    {
        public static void CopyText2Clipboard(string text)
        {
            GUIUtility.systemCopyBuffer = text;
        }
    }
}