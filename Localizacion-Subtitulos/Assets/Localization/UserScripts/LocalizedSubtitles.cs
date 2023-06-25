using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Localization
{

    public class LocalizedSubtitles : Localizable
    {
        protected override void Initialise()
        {
           // string fileContent = File.ReadAllText(LocalizationManager.Instance.GetSubtitle(key));
        }

        protected override void Localize()
        {
            //throw new System.NotImplementedException();
        }
    }

}
