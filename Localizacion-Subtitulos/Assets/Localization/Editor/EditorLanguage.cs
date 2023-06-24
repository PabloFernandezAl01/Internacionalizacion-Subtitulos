using UnityEngine;
using UnityEditor;
using System.IO;

namespace Localization
{
    /// <summary>
    /// Window to include new languages
    /// </summary>
    public class EditorLanguage : EditorWindow
    {

        [MenuItem("Localization/Languages")]
        public static void ShowWindow()
        {
            if (Localization.EditorUtility.TryOpenWindow())
                GetWindow(typeof(EditorLanguage));
        }

        private void OnDestroy()
        {
            Localization.EditorUtility.CloseWindow();
        }

        private void Awake()
        {
            titleContent.text = "Languages";
            //titleContent.image = Resources.Load("Localization/Globe") as Texture;
            minSize = new Vector2(400, 500);
            UpdateWindow();
        }


        private void UpdateWindow()
        {
            saveChangesMessage = "Do you wish to save the new changes?";

            languages = Localization.EditorUtility.LoadLanguages();
            defaultLanguage = Localization.EditorUtility.DefaultLanguage();

        }


        int defaultLanguage = 0;
        string[] languages;

        string newLanguage;
        bool addingLanguage = false;

        private void AddLanguage()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Add Language"))
            {
                addingLanguage = !addingLanguage;
                newLanguage = "New Language";
            }

            if (addingLanguage)
            {
                newLanguage = GUILayout.TextField(newLanguage, GUILayout.MinWidth(100));

                if (GUILayout.Button("+"))
                {
                    addingLanguage = false;

                    string[] newarray = new string[languages.Length + 1];

                    for (int i = 0; i < languages.Length; i++)
                    {
                        newarray[i] = languages[i];
                    }

                    newarray[languages.Length] = newLanguage;

                    languages = newarray;

                    hasUnsavedChanges = true;
                }
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void OnProjectChange()
        {
            UpdateWindow();
        }

        private void OnGUI()
        {
            // currentLanguage = EditorGUILayout.Popup("Language", currentLanguage, languages);
            Localization.EditorUtility.CreateIconTittle(position);

            AddLanguage();

            GUILayout.BeginVertical();

            int newLanguage = EditorGUILayout.Popup("Default language", defaultLanguage, languages);

            if(newLanguage != defaultLanguage)
            {
                defaultLanguage = newLanguage;
                hasUnsavedChanges = true;
            }

            GUILayout.EndVertical();
        }

        public override void SaveChanges()
        {
            base.SaveChanges();

            File.WriteAllText(Application.dataPath + "/Localization/Resources/LanguagesFile.txt", CreateLanguageFile());
            File.WriteAllText(Application.dataPath + "/Localization/Resources/LanguagesEnum.cs", CreateLanguageEnum());
            File.WriteAllText(Application.dataPath + "/Localization/Resources/DefaultLanguage.txt", defaultLanguage.ToString());

            AssetDatabase.Refresh();
        }

        private string CreateLanguageFile()
        {
            string str = "";

            foreach (string lang in languages)
            {
                str += lang + "\n";
            }

            if (languages.Length > 0)
                str = str.Remove(str.Length - 1, 1);

            return str;
        }

        private string CreateLanguageEnum()
        {
            string str = @"

namespace Localization
{
    public enum Language
    {
";
            str += "\t\t";

            foreach (string lang in languages)
            {
                str += lang.Replace(" ", "") + ", ";
            }

            str = str.Remove(str.Length - 2, 2);

            str += @"
    }
}";

            return str;
        }

        public override void DiscardChanges()
        {
            base.DiscardChanges();
        }
    }
}