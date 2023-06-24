using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Localization
{
    public abstract class Localizable : MonoBehaviour
    {
        public string key;

        /// <summary>
        /// Abstract method to be override in child classes
        /// </summary>
        /// 

        protected abstract void Localize();

        public void ChangeLanguage()
        {
            Localize();
            ValueChangeCallback();
        }

        protected abstract void Initialise();

        private void Start()
        {
            // Add the located text to the list in the manager
            LocalizationManager.Instance.AddLocalizble(this);

            Initialise();

            // Set the text by language
            ChangeLanguage();

            if (onValueChange == null)
                onValueChange = new UnityEvent<Localizable>();

        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.RemoveLocalizable(this);
        }

        private void ValueChangeCallback()
        {
            onValueChange?.Invoke(this);
        }

        public void Subscribe(UnityAction<Localizable> action)
        {
            onValueChange.AddListener(action);
        }

        public void ResetSubscribers()
        {
            onValueChange.RemoveAllListeners();
        }


        [SerializeField]
        private UnityEvent<Localizable> onValueChange;

    }
}