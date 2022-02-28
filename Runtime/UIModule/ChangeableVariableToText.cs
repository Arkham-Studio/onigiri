//using TMPro;
using UnityEngine.UI;


namespace Arkham.Onigiri.UI
{
    public class ChangeableVariableToText : BaseChangeableVariableToString<Text>
    {
        public override void UpdateMyText() => myText.text = target;
    }
}
