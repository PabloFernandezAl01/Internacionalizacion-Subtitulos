using UnityEngine;

public class Button : MonoBehaviour
{
    public static void ChangeLanguage(int value)
    {
        Localization.LocalizationManager m = Localization.LocalizationManager.Instance;
        Localization.Language l = (Localization.Language)value;

        if (m.GetCurrentLanguage() != l)
            m.ChangeLanguage(l);

    }
}
