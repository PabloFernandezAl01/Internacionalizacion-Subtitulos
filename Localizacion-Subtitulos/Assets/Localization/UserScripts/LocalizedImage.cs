using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace Localization
{
    public class LocalizedImage : Localizable
    {
        /*
            Actualmente no tiene soporte para rawImage
        */

        private Image image;
        private SpriteRenderer sprite;

        protected override void Initialise()
        {
            //Get reference
            image = GetComponent<Image>();
            sprite = GetComponent<SpriteRenderer>();
        }

        protected override void Localize()
        {
            Sprite newsprite = LocalizationManager.Instance.GetSprite(key);

            if (image != null)
            {
                image.sprite = newsprite;
            }

            if (sprite != null)
            {
                sprite.sprite = newsprite;
            }

        }

    }
}