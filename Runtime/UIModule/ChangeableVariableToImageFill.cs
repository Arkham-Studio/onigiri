using UnityEngine;
using UnityEngine.UI;

namespace Arkham.Onigiri.UI
{
    public class ChangeableVariableToImageFill : BaseChangeableVariableToFloat<Image>
    {
        public override void UpdateOnEvent() => myComponent.fillAmount = inverted ? 1 - targetValue : targetValue;
        public override void UpdateValueLerp() => myComponent.fillAmount = Mathf.Lerp(myComponent.fillAmount, inverted ? 1 - targetValue : targetValue, Time.fixedDeltaTime / smooth);
    } 
}
