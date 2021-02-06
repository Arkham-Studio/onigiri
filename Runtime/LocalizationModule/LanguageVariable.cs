using Arkham.Onigiri.Variables;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Language Variable")]
public class LanguageVariable : BaseVariable<SystemLanguage>
{

    public bool isDebug = false;

    public void ChangeLanguage(string v)
    {
        if (v == "") return;
        Value = (SystemLanguage)System.Enum.Parse(typeof(SystemLanguage), v, true);
        OnChange();
    }

    public void CheckSystemLanguage()
    {
        if (isDebug) return;
        Value = Application.systemLanguage == SystemLanguage.French ? SystemLanguage.French : SystemLanguage.English;
        OnChange();
    }

}