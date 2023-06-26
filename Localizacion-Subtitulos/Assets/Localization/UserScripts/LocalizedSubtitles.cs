using UnityEngine;

namespace Localization
{
    public class LocalizedSubtitles : Localizable
    {
        private SubtitlesManager subManager;

        protected override void Initialise()
        {
            subManager = GetComponent<SubtitlesManager>();

            if (!subManager)
                Debug.LogError("La entidad no contiene ningun componente de tipo SubtitlesManager");
        }

        protected override void Localize()
        {
            string configurationFile = LocalizationManager.Instance.GetSubtitle(key);

            subManager.fileName = configurationFile;

            subManager.LoadSubtitles();

        }
    }

}
