using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    public class LocalizedAudio : Localizable
    {
        private AudioSource audioReference;

        protected override void Initialise()
        {
            //Get reference
            audioReference = GetComponent<AudioSource>();
        }

        protected override void Localize()
        {
            audioReference.clip = LocalizationManager.Instance.GetAudio(key);
            audioReference.Play();
        }

    }
}


