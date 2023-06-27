using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Drawing;
using System;
using System.IO;
using System.Linq;

namespace Localization
{
    public class EditorExtras : EditorWindow
    {

        [MenuItem("Localization/Extras")]
        public static void ShowWindow()
        {
            if (Localization.EditorUtility.TryOpenWindow())
            {
                GetWindow(typeof(EditorExtras));
            }
        }

        private void OnDestroy()
        {
            Localization.EditorUtility.CloseWindow();


            foreach (var lang in languages)
            {
                UnityEditor.EditorUtility.SetDirty(lang);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }

        private void Awake()
        {
            titleContent.text = "Localization extra";
            //titleContent.image = Resources.Load("Localization/Globe") as Texture;
            minSize = new Vector2(400, 500);
            UpdateWindow();
        }

        private void OnProjectChange()
        {
            UpdateWindow();
        }

        private void UpdateWindow()
        {
            saveChangesMessage = "Do you wish to save the new changes?";

            languagesNames = Localization.EditorUtility.CreateDefaultOption(Localization.EditorUtility.LoadLanguages());
            languages = new LanguageAssets[languagesNames.Length];

            for (int i = 0; i < languagesNames.Length; i++)
            {
                languages[i] = Resources.Load("Languages/" + languagesNames[i]) as LanguageAssets;

                if (languages[i] == null)
                {
                    languages[i] = CreateInstance(typeof(LanguageAssets)) as LanguageAssets;
                    languages[i].Init();

                    string path = string.Format("Assets/Localization/Resources/Languages/{0}.asset", languagesNames[i]);

                    AssetDatabase.CreateAsset(languages[i], path);
                    AssetDatabase.SaveAssets();
                }

                if (!languages[i].extrasInitialised)
                {
                    Debug.Log("Inicializando extras");
                    languages[i].InitialiseExtras();
                }
            }
        }

        int currentLanguage = 0;
        string[] languagesNames;
        LanguageAssets[] languages;

        private void OnGUI()
        {
            // currentLanguage = EditorGUILayout.Popup("Language", currentLanguage, languages);
            Localization.EditorUtility.CreateIconTittle(position);

            GUILayout.BeginVertical();

            currentLanguage = GUILayout.Toolbar(currentLanguage, languagesNames);

            GUILayout.EndVertical();


            DisplayLanguage(currentLanguage);

            if(GUILayout.Button("Save Data"))
            {
                saveLanguages();
                Debug.Log("Saving files in CSV");
            }

            if (GUILayout.Button("Load Data"))
            {
                loadLanguages();
                Debug.Log("Loading files in CSV");
            }
        }

        public void saveLanguages()
        {
            for (int i = 0; i < languages.Length; i++)
            {
                saveLanguage(languagesNames[i]);
            }
        }

        private void saveLanguage(string lang)
        {
            LanguageAssets language = Resources.Load("Languages/" + lang) as LanguageAssets;

            Dictionary<string, string> texts = language.CreateTextDictionaty();

            String csv = String.Join(
                 Environment.NewLine,
                 texts.Select(d => $"{d.Key};{d.Value};")
                );


            File.WriteAllText(Application.dataPath + "/Localization/CSV/" + lang, csv);
        }

        public void loadLanguages()
        {
            for (int i = 0; i < languages.Length; i++)
            {
                loadLanguage(languagesNames[i], i);
            }
        }

        private void loadLanguage(string lang, int ind)
        {

            Dictionary<string, string> dict = new Dictionary<string, string>();

            StreamReader reader = new StreamReader(Application.dataPath + "/Localization/CSV/" + lang);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                string left = line.Split(';')[0].Trim();
                string right = line.Split(';')[1].Trim();

                dict.Add(left, right);
            }

            reader.Close();

            languages[ind].addTextList(dict);
        }

        private void DisplayLanguage(int idx)
        {

            GUILayout.BeginVertical();
            GUILayout.Label("Currency");

            GUILayout.BeginHorizontal();

            languages[idx].extras.currency = GUILayout.TextField(languages[idx].extras.currency, 3);
            languages[idx].extras.currencySuffix = GUILayout.Toggle(languages[idx].extras.currencySuffix, "Is the currency symbol a suffix?");

            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Decimal separator");
            languages[idx].extras.decimalSeparator = GUILayout.TextField(languages[idx].extras.decimalSeparator.ToString(), 1);

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

        }

        public override void SaveChanges()
        {
            base.SaveChanges();
            AssetDatabase.Refresh();
        }

        public override void DiscardChanges()
        {
            base.DiscardChanges();
        }

    }
}