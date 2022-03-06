//using TMPro;
using UnityEngine.UI;
using TMPro;


namespace Arkham.Onigiri.UI
{
    public class ChangeableVariableToTMP : BaseChangeableVariableToString<TextMeshProUGUI>
    {
        public override void UpdateMyText() => myText.text = target;
    }
}
