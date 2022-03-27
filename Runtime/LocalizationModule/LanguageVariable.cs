#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using UnityEngine;

namespace Arkham.Onigiri.LocalizationModule
{
    [CreateAssetMenu(menuName = "Variables/Language Variable")]
    public class LanguageVariable : BaseVariable<SystemLanguage>
    {

        public bool isDebug = false;

        public void ChangeLanguage(string v)
        {
            if (v == "") return;
            currentValue = ((SystemLanguage)System.Enum.Parse(typeof(SystemLanguage), v, true));
            OnChange();
        }

        public void CheckSystemLanguage()
        {
            if (isDebug) return;
            currentValue = (Application.systemLanguage == SystemLanguage.French ? SystemLanguage.French : SystemLanguage.English);
            OnChange();
        }

    }
}