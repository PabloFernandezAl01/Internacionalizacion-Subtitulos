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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    [Serializable]
    public class SpritePair : LocalizedKeyPair<Sprite> {}

    [Serializable]
    public class AudioClipPair : LocalizedKeyPair<AudioClip> {}

    [Serializable]
    public class FontPair : LocalizedKeyPair<Font> {}

    [Serializable]
    public class TMPFontPair : LocalizedKeyPair<TMP_FontAsset> {}

    [Serializable]
    public class SubtitlePair : StringPair { }

    public enum AssetType
    {
        Text, Sprite, Audio, Font, FontTMP, Subtitle
    }


    [CreateAssetMenu(fileName = "Language", menuName = "Localization/Language")]
    public class LanguageAssets : ScriptableObject
    {
        public List<StringPair> texts;
        public List<SpritePair> sprites;

        public List<AudioClipPair> audios;
        public List<FontPair> fonts;
        public List<TMPFontPair> tmpFonts;
        public List<SubtitlePair> subtitles;

        public bool extrasInitialised;
        public LanguageExtras extras;

        public void Init()
        {
            texts = new List<StringPair>();
            sprites = new List<SpritePair>();
            audios = new List<AudioClipPair>();
            fonts = new List<FontPair>();
            subtitles = new List<SubtitlePair>();

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
            Dictionary<string, string> dic = new Dictionary<string, string>();

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
            Dictionary<string, Sprite> dic = new Dictionary<string, Sprite>();

            foreach (var spr in sprites)
            {
                dic.Add(spr.Key, spr.Value);
            }

            return dic;
        }


        public Dictionary<string, AudioClip> CreateAudioDictionaty()
        {
            Dictionary<string, AudioClip> dic = new Dictionary<string, AudioClip>();

            foreach (var aud in audios)
            {
                dic.Add(aud.Key, aud.Value);
            }

            return dic;
        }


        public Dictionary<string, Font> CreateFontDictionaty()
        {
            Dictionary<string, Font> dic = new Dictionary<string, Font>();

            foreach (var ft in fonts)
            {
                dic.Add(ft.Key, ft.Value);
            }

            return dic;
        }


        public Dictionary<string, TMP_FontAsset> CreateTMPFontDictionaty()
        {
            Dictionary<string, TMP_FontAsset> dic = new Dictionary<string, TMP_FontAsset>();

            foreach (var ft in tmpFonts)
            {
                dic.Add(ft.Key, ft.Value);
            }

            return dic;
        }

        public Dictionary<string, string> CreateSubtitleDictionaty()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (var st in subtitles)
            {
                dic.Add(st.Key, st.Value);
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