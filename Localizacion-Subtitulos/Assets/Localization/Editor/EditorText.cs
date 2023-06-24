using UnityEngine;
using UnityEditor;

namespace Localization
{
    public class EditorText : EditorKey
    {
        [MenuItem("Localization/KeyCreator/Text")]
        public static void ShowWindow()
        {
            if (Localization.EditorUtility.TryOpenWindow())
            {
                GetWindow(typeof(EditorText));
            }
        }

        protected override void Initialise()
        {
            assetType = AssetType.Text;
            titleContent.text = "Text keys and values";
        }
    }
}