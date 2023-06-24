using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Drawing;

namespace Localization
{
    public abstract class EditorKey : EditorWindow
    {
        protected AssetType assetType;

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

        protected abstract void Initialise();

        private void Awake()
        {
            titleContent.text = "Change keys and values";
            //titleContent.image = Resources.Load("Localization/Globe") as Texture;
            minSize = new Vector2(400, 500);
            UpdateWindow();

            Initialise();
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
            }
        }

        int currentLanguage = 0;
        string[] languagesNames;
        LanguageAssets[] languages;

        string newKey = "New Key";

        private void OnGUI()
        {
            // currentLanguage = EditorGUILayout.Popup("Language", currentLanguage, languages);
            Localization.EditorUtility.CreateIconTittle(position);

            GUILayout.BeginVertical();

            currentLanguage = GUILayout.Toolbar(currentLanguage, languagesNames);

            GUILayout.EndVertical();


            DisplayLanguage(currentLanguage);
        }

        private bool ContainsKey(string key)
        {

            switch (assetType)
            {
                case AssetType.Text:

                    for (int i = 0; i < languages[0].texts.Count; i++)
                    {
                        if (languages[0].texts[i].Key == key)
                            return true;
                    }

                    break;
                case AssetType.Sprite:

                    for (int i = 0; i < languages[0].sprites.Count; i++)
                    {
                        if (languages[0].sprites[i].Key == key)
                            return true;
                    }

                    break;
                case AssetType.Audio:

                    for (int i = 0; i < languages[0].audios.Count; i++)
                    {
                        if (languages[0].audios[i].Key == key)
                            return true;
                    }

                    break;
                case AssetType.Font:

                    for (int i = 0; i < languages[0].fonts.Count; i++)
                    {
                        if (languages[0].fonts[i].Key == key)
                            return true;
                    }

                    break;

                case AssetType.FontTMP:

                    for (int i = 0; i < languages[0].tmpFonts.Count; i++)
                    {
                        if (languages[0].tmpFonts[i].Key == key)
                            return true;
                    }

                    break;
            }

            return false;
        }


        private void AddKey(string key)
        {
            if (ContainsKey(key))
                return;

            foreach (var lang in languages)
            {
                switch (assetType)
                {
                    case AssetType.Text:
                        lang.texts.Add(new StringPair { Key = key, Value = "@" });
                        break;
                    case AssetType.Sprite:
                        lang.sprites.Add(new SpritePair { Key = key, Value = null });
                        break;
                    case AssetType.Audio:
                        lang.audios.Add(new AudioClipPair { Key = key, Value = null });
                        break;
                    case AssetType.Font:
                        lang.fonts.Add(new FontPair { Key = key, Value = null });
                        break;
                    case AssetType.FontTMP:
                        lang.tmpFonts.Add(new TMPFontPair { Key = key, Value = null });
                        break;
                }

            }
        }

        private void RemoveKey(string key)
        {
            foreach (var lang in languages)
            {
                switch (assetType)
                {
                    case AssetType.Text:

                        for (int i = lang.texts.Count - 1; i >= 0; i--)

                            if (lang.texts[i].Key == key)
                            {
                                lang.texts.Remove(lang.texts[i]);
                                break;
                            }

                        break;
                    case AssetType.Sprite:

                        for (int i = lang.sprites.Count - 1; i >= 0; i--)

                            if (lang.sprites[i].Key == key)
                            {
                                lang.sprites.Remove(lang.sprites[i]);
                                break;
                            }

                        break;
                    case AssetType.Audio:

                        for (int i = lang.audios.Count - 1; i >= 0; i--)

                            if (lang.audios[i].Key == key)
                            {
                                lang.audios.Remove(lang.audios[i]);
                                break;
                            }


                        break;
                    case AssetType.Font:

                        for (int i = lang.fonts.Count - 1; i >= 0; i--)

                            if (lang.fonts[i].Key == key)
                            {
                                lang.fonts.Remove(lang.fonts[i]);
                                break;
                            }

                        break;

                    case AssetType.FontTMP:

                        for (int i = lang.tmpFonts.Count - 1; i >= 0; i--)

                            if (lang.tmpFonts[i].Key == key)
                            {
                                lang.tmpFonts.Remove(lang.tmpFonts[i]);
                                break;
                            }

                        break;
                }

            }
        }

        private void DisplayLanguage(int idx)
        {

            if (idx == 0)
            {
                GUILayout.BeginHorizontal();

                newKey = GUILayout.TextField(newKey);

                if (GUILayout.Button("Add key"))
                {
                    AddKey(newKey);

                    foreach (var lang in languages)
                    {
                        UnityEditor.EditorUtility.SetDirty(lang);
                    }
                }

                GUILayout.EndHorizontal();
            }


            List<string> keysToRemove = new();

            switch (assetType)
            {
                case AssetType.Text:

                    foreach (var txt in languages[idx].texts)
                    {
                        ShowKeysString(txt, keysToRemove);
                    }

                    break;
                case AssetType.Sprite:

                    foreach (var spr in languages[idx].sprites)
                    {
                        ShowKeys(spr, keysToRemove);
                    }

                    break;
                case AssetType.Audio:

                    foreach (var aud in languages[idx].audios)
                    {
                        ShowKeys(aud, keysToRemove);
                    }

                    break;
                case AssetType.Font:

                    foreach (var font in languages[idx].fonts)
                    {
                        ShowKeys(font, keysToRemove);
                    }

                    break;

                case AssetType.FontTMP:

                    foreach (var font in languages[idx].tmpFonts)
                    {
                        ShowKeys(font, keysToRemove);
                    }

                    break;
            }

            foreach (var spr in keysToRemove)
                RemoveKey(spr);
        }

        private void ShowKeysString(StringPair str, List<string> keysToRemove)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(str.Key);

            string value = GUILayout.TextField(str.Value);

            if (value != str.Value)
            {
                str.Value = value;
            }

            if (GUILayout.Button("Remove key"))
            {
                keysToRemove.Add(str.Key);
            }

            GUILayout.EndHorizontal();
        }

        private void ShowKeys<T>(LocalizedKeyPair<T> keypair, List<string> keysToRemove) where T : Object
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(keypair.Key);

            T value = EditorGUILayout.ObjectField(keypair.Value, typeof(T), false) as T;

            if (value != keypair.Value)
            {
                keypair.Value = value;
            }

            if (GUILayout.Button("Remove key"))
            {
                keysToRemove.Add(keypair.Key);
            }

            GUILayout.EndHorizontal();
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