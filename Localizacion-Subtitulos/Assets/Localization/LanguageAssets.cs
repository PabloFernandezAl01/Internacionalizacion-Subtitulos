using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Localization
{
    [Serializable]
    public class LocalizedKeyPair<T>
    {
        public string Key;
        public T Value;

    }

    [Serializable]
    public class StringPair : LocalizedKeyPair<string>
    {
        public static bool operator == (StringPair obj1, StringPair obj2)
        {
            return (obj1.Key == obj2.Key
                        && obj1.Value == obj2.Value);
        }

        public static bool operator !=(StringPair obj1, StringPair obj2)
        {
            return (obj1.Key == obj2.Key
                        && obj1.Value == obj2.Value);
        }
    }

    [Serializable]
    public class SpritePair : LocalizedKeyPair<Sprite>
    {
    }

    [Serializable]
    public class AudioClipPair : LocalizedKeyPair<AudioClip>
    {
    }

    [Serializable]
    public class FontPair : LocalizedKeyPair<Font>
    {
    }

    [Serializable]
    public class TMPFontPair : LocalizedKeyPair<TMP_FontAsset>
    {
    }

    public enum AssetType
    {
        Text, Sprite, Audio, Font, FontTMP
    }

    [CreateAssetMenu(fileName = "Language", menuName = "Localization/Language")]
    public class LanguageAssets : ScriptableObject
    {
        public List<StringPair> texts;
        public List<SpritePair> sprites;

        public List<AudioClipPair> audios;
        public List<FontPair> fonts;
        public List<TMPFontPair> tmpFonts;

        public bool extrasInitialised;
        public LanguageExtras extras;

        public void Init()
        {
            texts = new();
            sprites = new();
            audios = new();
            fonts = new();

            extras = new LanguageExtras();

            InitialiseExtras();
        }

        public void InitialiseExtras()
        {
            extras.currencySuffix = true;
            extras.currency = "@";
            extras.decimalSeparator = ".";
            extrasInitialised = true;
        }

        public void addTextList(Dictionary<string, string> newText)
        {
            foreach (var txt in newText)
            {
                StringPair aux = new StringPair();
                aux.Key = txt.Key;
                aux.Value = txt.Value;

                bool exist = false;
                foreach (var pair in texts)
                {

                    if (pair == aux)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    texts.Add(aux);
                }
            }
        }

        public Dictionary<string, string> CreateTextDictionaty()
        {
            Dictionary<string, string> dic = new();

            foreach (var txt in texts)
            {
                if(!dic.ContainsKey(txt.Key))
                {
                    dic.Add(txt.Key, txt.Value);
                }
            }

            return dic;
        }

        public Dictionary<string, Sprite> CreateImageDictionaty()
        {
            Dictionary<string, Sprite> dic = new();

            foreach (var spr in sprites)
            {
                dic.Add(spr.Key, spr.Value);
            }

            return dic;
        }


        public Dictionary<string, AudioClip> CreateAudioDictionaty()
        {
            Dictionary<string, AudioClip> dic = new();

            foreach (var aud in audios)
            {
                dic.Add(aud.Key, aud.Value);
            }

            return dic;
        }


        public Dictionary<string, Font> CreateFontDictionaty()
        {
            Dictionary<string, Font> dic = new();

            foreach (var ft in fonts)
            {
                dic.Add(ft.Key, ft.Value);
            }

            return dic;
        }


        public Dictionary<string, TMP_FontAsset> CreateTMPFontDictionaty()
        {
            Dictionary<string, TMP_FontAsset> dic = new();

            foreach (var ft in tmpFonts)
            {
                dic.Add(ft.Key, ft.Value);
            }

            return dic;
        }



    }


    [System.Serializable]
    public class LanguageExtras
    {
        public bool currencySuffix;
        public string currency;

        public string decimalSeparator;

    }
}