using UnityEditor;

namespace Localization
{
    public class EditorSubtitle : EditorKey
    {
        [MenuItem("Localization/KeyCreator/Subtitle")]
        public static void ShowWindow()
        {
            if (Localization.EditorUtility.TryOpenWindow())
            {
                GetWindow(typeof(EditorSubtitle));
            }
        }

        protected override void Initialise()
        {
            assetType = AssetType.Subtitle;
            titleContent.text = "Subtitle keys and values";
        }
    }
}
