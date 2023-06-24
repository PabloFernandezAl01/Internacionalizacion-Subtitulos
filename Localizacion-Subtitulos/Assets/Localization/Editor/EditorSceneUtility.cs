using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class EditorSceneUtility : EditorWindow
    {
        public static bool WindowOpened = false;

        [MenuItem("Localization/Scene Utility")]
        public static void ShowWindow()
        {
            GetWindow(typeof(EditorSceneUtility));
        }

        private void Awake()
        {
            languages = EditorUtility.LoadLanguages();
            titleContent.text = "Scene Utility";
            maxSize = new Vector2(600, maxSize.y);
        }

        string[] languages;
        private void OnGUI()
        {
            GUILayout.BeginVertical();

            if (GUILayout.Button("Localize current scene"))
            {
                LocalizeCurrentScene();
            }

            GUI.enabled = !Application.isPlaying;

            GUILayout.Label("Translate current scene: " + (!Application.isPlaying ? "" : " (Not avaliable during play mode)"));

            for (int i = 0; i < languages.Length; i++)
            {

                if (GUILayout.Button(languages[i]))
                {
                    SetSceneLanguage(languages[i]);
                }
            }

            GUI.enabled = Application.isPlaying;

            GUILayout.Label("Change localization manager language: " + (Application.isPlaying ? "" : " (Only avaliable during play mode)"));

            for (int i = 0; i < languages.Length; i++)
            {
                if (GUILayout.Button(languages[i]))
                {
                    ChangeCurrentLanguage((Language)i);
                }
            }
            GUI.enabled = true;

            GUILayout.EndVertical();
        }


            private void SetSceneLanguage(string lang)
        {
            LanguageAssets language = Resources.Load("Languages/" + lang) as LanguageAssets;
            LanguageAssets defaultLanguage = Resources.Load("Languages/Default") as LanguageAssets;

            LocalizedText[] localizedTexts = GameObject.FindObjectsOfType<LocalizedText>();
            LocalizedImage[] localiezedImages = GameObject.FindObjectsOfType<LocalizedImage>();

            Dictionary<string, string> texts = language.CreateTextDictionaty();
            Dictionary<string, string> defaultTexts = defaultLanguage.CreateTextDictionaty();

            Dictionary<string, Sprite> images = language.CreateImageDictionaty();
            Dictionary<string, Sprite> defaultImages = defaultLanguage.CreateImageDictionaty();


            for (int i = 0; i < localizedTexts.Length; i++)
            {
                string txt = "";

                if (texts.ContainsKey(localizedTexts[i].key) && texts[localizedTexts[i].key] != "@")
                {
                    txt = texts[localizedTexts[i].key];
                }
                else if (defaultTexts.ContainsKey(localizedTexts[i].key))
                {
                    txt = defaultTexts[localizedTexts[i].key];
                }

                Text text = localizedTexts[i].GetComponent<Text>();
                if (text)
                {
                    Undo.RecordObject(text, "Translated text");
                    text.text = txt;
                }

                TMP_Text tmp = localizedTexts[i].GetComponent<TMP_Text>();
                if (tmp)
                {
                    Undo.RecordObject(tmp, "Translated text (tmp)");
                    tmp.text = txt;
                }
            }


            for (int i = 0; i < localiezedImages.Length; i++)
            {
                Sprite spr = null;

                if (images.ContainsKey(localiezedImages[i].key) && images[localiezedImages[i].key] != null)
                {
                    spr = images[localiezedImages[i].key];
                }
                else if (defaultImages.ContainsKey(localiezedImages[i].key))
                {
                    spr = defaultImages[localizedTexts[i].key];
                }

                Image image = localiezedImages[i].GetComponent<Image>();
                if (image)
                {
                    Undo.RecordObject(image, "Localized image");
                    image.sprite = spr;
                }

                SpriteRenderer spriteRenderer = localizedTexts[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer)
                {
                    Undo.RecordObject(spriteRenderer, "Localized sprite renderer");
                    spriteRenderer.sprite = spr;
                }
            }


            SceneView.RepaintAll();
        }

        private void ChangeCurrentLanguage(Language language)
        {
            if (!Application.isPlaying)
            {
                UnityEditor.EditorUtility.DisplayDialog(
                 "Application not running",
                 "You cannot change the current language if the application is not running", ":(");
                return;
            }

            LocalizationManager.TryGetInstance?.ChangeLanguage(language);
        }


        private void LocalizeCurrentScene()
        {
            Text[] texts = GameObject.FindObjectsOfType<Text>();

            foreach (Text text in texts)
            {
                LocalizedText localized = text.GetComponent<LocalizedText>();

                if (!localized)
                {
                    Undo.RecordObject(text.gameObject, "Created component localized text");

                    localized = text.gameObject.AddComponent<LocalizedText>();
                    localized.key = text.text;
                }
            }

            TMP_Text[] tmps = GameObject.FindObjectsOfType<TMP_Text>();

            foreach (TMP_Text text in tmps)
            {
                LocalizedText localized = text.GetComponent<LocalizedText>();

                if (!localized)
                {
                    Undo.RecordObject(text.gameObject, "Created component localized text (TMP)");

                    localized = text.gameObject.AddComponent<LocalizedText>();
                    localized.key = text.text;
                }
            }


            Image[] images = GameObject.FindObjectsOfType<Image>();

            foreach (Image image in images)
            {
                LocalizedImage localized = image.GetComponent<LocalizedImage>();

                if (!localized)
                {
                    Undo.RecordObject(image.gameObject, "Created component localized Image");

                    localized = image.gameObject.AddComponent<LocalizedImage>();
                    localized.key = "Default";
                }
            }

            SpriteRenderer[] sprites = GameObject.FindObjectsOfType<SpriteRenderer>();

            foreach (SpriteRenderer spr in sprites)
            {
                LocalizedImage localized = spr.GetComponent<LocalizedImage>();

                if (!localized)
                {
                    Undo.RecordObject(spr.gameObject, "Created component localized Image");

                    localized = spr.gameObject.AddComponent<LocalizedImage>();
                    localized.key = "Default";
                }
            }

            AudioSource[] audiosSrc = GameObject.FindObjectsOfType<AudioSource>();

            foreach (AudioSource audio in audiosSrc)
            {
                LocalizedAudio localized = audio.GetComponent<LocalizedAudio>();

                if (!localized)
                {
                    Undo.RecordObject(audio.gameObject, "Created component localized Audio");

                    localized = audio.gameObject.AddComponent<LocalizedAudio>();
                    localized.key = "Default";
                }
            }

        }

    }

}
