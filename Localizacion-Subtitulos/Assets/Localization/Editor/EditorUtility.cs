using UnityEngine;
using UnityEditor;
using System;

namespace Localization
{
    public static class EditorUtility 
    {

        private static bool windowOpened = false;

        public static bool TryOpenWindow()
        {
            if (windowOpened)
            {
                UnityEditor.EditorUtility.DisplayDialog(
                    "Localization already in use",
                    "You can only have one localization window opened at the same time",
                    ":(");

                return false;
            }

            return windowOpened = true;
        }

        public static void CloseWindow()
        {
            windowOpened = false;
        }

        public static int DefaultLanguage()
        {
            TextAsset text = Resources.Load("DefaultLanguage") as TextAsset;

            if (text == null)
            {
                return 0;
            }

            return int.Parse(text.text);
        }

        public static string[] LoadLanguages()
        {
            TextAsset text = Resources.Load("LanguagesFile") as TextAsset;

            if (text == null)
            {
                text = new TextAsset("English");
            }

            var split = text.text.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            for(int i = 0; i < split.Length; i++)
            {
                split[i] = split[i].Trim(new char []{ ' ', '\n', '\r'});
            }
            return split;
        }

        public static void CreateIconTittle(Rect position)
        {
            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            string texture = position.width < 600 ? "Globe" : "Logo";

            Texture2D globe = Resources.Load("LocalizationAssets/" + texture) as Texture2D;
            GUILayout.Label(globe, new GUIStyle() { alignment = TextAnchor.UpperCenter }, GUILayout.MaxWidth(position.width), GUILayout.MaxHeight(position.height * 0.4f));

            GUILayout.FlexibleSpace();


            GUILayout.EndHorizontal();
        }

        public static string[] CreateDefaultOption(string[] languages)
        {
            string[] withDefault = new string[languages.Length + 1];

            withDefault[0] = "Default";

            for (int i = 0; i < languages.Length; i++)
            {
                withDefault[i + 1] = languages[i];
            }

            return withDefault;
        }

    }

}