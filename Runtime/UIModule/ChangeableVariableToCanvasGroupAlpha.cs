using UnityEngine;

namespace Arkham.Onigiri.UI
{
    public class ChangeableVariableToCanvasGroupAlpha : BaseChangeableVariableToFloat<CanvasGroup>
    {
        public override void UpdateOnEvent() => myComponent.alpha = inverted ? 1 - targetValue : targetValue;
        public override void UpdateValueLerp() => myComponent.alpha = Mathf.Lerp(myComponent.alpha, inverted ? 1 - targetValue : targetValue, Time.fixedDeltaTime / smooth);
    }
}