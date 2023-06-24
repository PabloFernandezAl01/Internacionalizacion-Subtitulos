using UnityEngine;
using UnityEditor;

namespace Localization
{
    public class EditorFont : EditorKey
    {
        [MenuItem("Localization/KeyCreator/Font")]
        public static void ShowWindow()
        {
            if (Localization.EditorUtility.TryOpenWindow())
            {
                GetWindow(typeof(EditorFont));
            }
        }

        protected override void Initialise()
        {
            assetType = AssetType.Font;
            titleContent.text = "Font keys and values";
        }
    }
}