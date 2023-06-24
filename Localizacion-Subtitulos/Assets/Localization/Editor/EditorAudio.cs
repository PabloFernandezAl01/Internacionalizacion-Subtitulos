using UnityEngine;
using UnityEditor;

namespace Localization
{
    public class EditorAudio : EditorKey
    {
        [MenuItem("Localization/KeyCreator/Audio")]
        public static void ShowWindow()
        {
            if (Localization.EditorUtility.TryOpenWindow())
            {
                GetWindow(typeof(EditorAudio));
            }
        }

        protected override void Initialise()
        {
            assetType = AssetType.Audio;
            titleContent.text = "Audio keys and values";
        }
    }
}