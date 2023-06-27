using UnityEngine;

namespace Localization
{
    public class LocalizedSubtitles : Localizable
    {
        private Subtitles subtitles;

        protected override void Initialise()
        {
            subtitles = GetComponent<Subtitles>();

            if (!subtitles)
                Debug.LogError("La entidad no contiene ningun componente de tipo SubtitlesManager");
        }

        protected override void Localize()
        {
            string configurationFile = LocalizationManager.Instance.GetSubtitle(key);

            subtitles.SetSubtitlesFile(configurationFile);

            subtitles.LoadSubtitles();

            subtitles.BeginSubtitles();

        }
    }

}
