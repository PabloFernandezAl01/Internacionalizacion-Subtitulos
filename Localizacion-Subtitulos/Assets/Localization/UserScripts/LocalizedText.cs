using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Localization
{
    public class LocalizedText : Localizable
    {
        private TMP_Text tmp = null;
        private Text text = null;

        public string FontKey = "";

        protected override void Initialise()
        {
            // Get the text reference
            tmp = GetComponent<TMP_Text>();
            text = GetComponent<Text>();

            if (text)
                text.resizeTextForBestFit = true;
        }

        protected override void Localize()
        {
            string newtext = LocalizationManager.Instance.GetText(key);

            if (text)
            {
                text.text = newtext;

                if (FontKey != "")
                {
                    text.font = LocalizationManager.Instance.GetFont(FontKey);
                }
            }

            if (tmp)
            {
                tmp.text = newtext;

                if (FontKey != "")
                {
                    tmp.font = LocalizationManager.Instance.GetTMPFont(FontKey);
                }
            }

        }

    }
}
