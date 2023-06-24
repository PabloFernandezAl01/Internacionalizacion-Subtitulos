using UnityEngine;
using UnityEditor;

namespace Localization
{
    public class EditorFontTMP : EditorKey
    {
        [MenuItem("Localization/KeyCreator/Font (Text mesh pro)")]
        public static void ShowWindow()
        {
            if (Localization.EditorUtility.TryOpenWindow())
            {
                GetWindow(typeof(EditorFontTMP));
            }
        }

        protected override void Initialise()
        {
            assetType = AssetType.FontTMP;
            titleContent.text = "Text mesh pro font keys and values";
        }
    }
}