using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Localization
{
    public class EditorImage : EditorKey
    {
        [MenuItem("Localization/KeyCreator/Image")]
        public static void ShowWindow()
        {
            if (Localization.EditorUtility.TryOpenWindow())
            {
                GetWindow(typeof(EditorImage));
            }
        }

        protected override void Initialise()
        {
            assetType = AssetType.Sprite;
            titleContent.text = "Images keys and values";
        }
    }
}