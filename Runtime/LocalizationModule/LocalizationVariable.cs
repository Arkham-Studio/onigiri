using UnityEngine;

namespace Arkham.Onigiri.LocalizationModule
{
    [CreateAssetMenu(menuName = "Variables/Localization Variable")]
    public class LocalizationVariable : ScriptableObject
    {
        public LanguageVariable actualLanguage;

        public bool fullCap;

        [Multiline]
        public string fr;

        [Multiline]
        public string en;

        public string Value()
        {
            if (actualLanguage.Value == SystemLanguage.English && en != "") return fullCap ? en.ToUpper() : en;
            else return fullCap ? fr.ToUpper() : fr;
        }

    }
}
