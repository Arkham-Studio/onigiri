//using TMPro;
using UnityEngine.UI;
using TMPro;
using Arkham.Onigiri.Variables;


namespace Arkham.Onigiri.UI.TMP
{
    public class ChangeableVariableToTMP : BaseChangeableVariableToString<TextMeshProUGUI>
    {
        public override void UpdateMyText() => myText.text = target;
    }
}
