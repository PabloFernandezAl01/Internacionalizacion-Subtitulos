using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

namespace Localization
{
    class LanguageDictionaries
    {
        public Dictionary<string, string> text { get; private set; }
        public Dictionary<string, Sprite> sprites { get; private set; }
        public Dictionary<string, AudioClip> audio { get; private set; }
        public Dictionary<string, Font> font { get; private set; }
        public Dictionary<string, TMP_FontAsset> TMPfont { get; private set; }
        public Dictionary<string, string> subtitles { get; private set; }

        public LanguageDictionaries(LanguageAssets language)
        {
            text = language.CreateTextDictionaty();
            sprites = language.CreateImageDictionaty();
            audio = language.CreateAudioDictionaty();
            font = language.CreateFontDictionaty();
            TMPfont = language.CreateTMPFontDictionaty();
            subtitles = language.CreateSubtitleDictionaty();
        }

    }

    public class LocalizationManager
    {

        private static LocalizationManager instance;
        public static LocalizationManager Instance
        {
            get
            {
                if (instance == null)
                    Init();

                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        // Esta llamada a la instancia no la crea en caso de que no exista
        public static LocalizationManager TryGetInstance
        {
            get
            {
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        private Language currentLanguage = Language.English;

        private List<Localizable> localizedObjects;

        public static void Init()
        {
            Instance = new LocalizationManager();
        }

        private LocalizationManager()
        {
            localizedObjects = new List<Localizable>();

            TextAsset text = Resources.Load("DefaultLanguage") as TextAsset;

            if (text == null)
            {
                currentLanguage = (Language)0;
            }
            else
                currentLanguage = (Language)int.Parse(text.text);

            LoadDefaultDictionaries();
            LoadAssets();
        }

        public Language GetCurrentLanguage()
        {
            return currentLanguage;
        }

        public void AddLocalizble(Localizable localizable)
        {
            localizedObjects.Add(localizable);
        }

        public void RemoveLocalizable(Localizable localizable)
        {
            localizedObjects.Remove(localizable);
        }

        public void ChangeLanguage(Language language)// (Localization.Language lang) Ver si se puede usar el enum aqui
        {
            Instance.LoadLanguage(language);
        }

        LanguageDictionaries languageAssets;
        LanguageDictionaries defaultAssets;

        LanguageExtras languageExtras;
        LanguageExtras defaultLanguageExtras;


        private void LoadDefaultDictionaries()
        {
            LanguageAssets languageInfo = Resources.Load("Languages/Default") as LanguageAssets;

            defaultAssets = new LanguageDictionaries(languageInfo);
            defaultLanguageExtras = languageInfo.extras;
        }

        private void LoadAssets()
        {
            LanguageAssets languageInfo = Resources.Load("Languages/" + currentLanguage.ToString()) as LanguageAssets;

            languageAssets = new LanguageDictionaries(languageInfo);
            languageExtras = languageInfo.extras;
        }

        public string DecimalSeparator(float value)
        {
            string newSeparator = languageExtras.decimalSeparator;

            if (newSeparator == "@")
                newSeparator = defaultLanguageExtras.decimalSeparator;

            return value.ToString(System.Globalization.CultureInfo.InvariantCulture).Replace(".", newSeparator);
        }

        public string TranslateCurrency(float value)
        {
            bool isSufix = languageExtras.currencySuffix;
            string currencySymbol = languageExtras.currency;

            if (currencySymbol == "@")
            {
                currencySymbol = defaultLanguageExtras.currency;
                isSufix = defaultLanguageExtras.currencySuffix;
            }

            string numberFormatted = DecimalSeparator(value);

            if (isSufix)
                return numberFormatted + currencySymbol;

            return currencySymbol + numberFormatted;
        }


        private void LoadLanguage(Localization.Language lang)
        {
            currentLanguage = lang;
            LoadAssets();


            // Change the texts
            foreach (var localizable in localizedObjects)
            {
                localizable.ChangeLanguage();
            }


            Debug.Log(Instance.TranslateCurrency(19.5f));
        }

        public AudioClip GetAudio(string key)
        {
            if (languageAssets.audio.ContainsKey(key))
            {
                AudioClip clip = languageAssets.audio[key];

                if (clip)
                    return clip;

                return defaultAssets.audio[key];
            }

            return null;
        }

        public Font GetFont(string key)
        {
            if (languageAssets.font.ContainsKey(key))
            {
                Font font = languageAssets.font[key];

                if (font)
                    return font;

                return defaultAssets.font[key];
            }

            return null;
        }

        public TMP_FontAsset GetTMPFont(string key)
        {
            if (languageAssets.TMPfont.ContainsKey(key))
            {
                TMP_FontAsset font = languageAssets.TMPfont[key];

                if (font)
                    return font;

                return defaultAssets.TMPfont[key];
            }

            return null;
        }



        public string GetText(string key)
        {
            if (languageAssets.text.ContainsKey(key))
            {
                string txt = languageAssets.text[key];

                if (txt != "@")
                    return txt;

                return defaultAssets.text[key];
            }

            return "";
        }


        public Sprite GetSprite(string key)
        {
            if (languageAssets.sprites.ContainsKey(key))
            {
                Sprite spr = languageAssets.sprites[key];

                if (spr)
                    return spr;

                return defaultAssets.sprites[key];
            }

            return null;
        }

        public string GetSubtitle(string key)
        {
            if (languageAssets.subtitles.ContainsKey(key))
            {
                string sub = languageAssets.subtitles[key];

                if (sub != "")
                    return sub;

                return defaultAssets.subtitles[key];
            }

            return null;
        }

    }
}