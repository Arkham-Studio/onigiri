using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Arkham.Onigiri.UI
{
    public class ChangeableVariableToSlider : BaseChangeableVariableToFloat<Slider>
    {
        public override void UpdateOnFloat(FloatVariable _v) => targetValue = _v.Value;
        public override void UpdateOnInt(IntVariable _v) => targetValue = _v.Value;
        public override void UpdateOnBool(BoolVariable _v) => targetValue = _v.Value ? myComponent.maxValue : myComponent.minValue;
        public override void UpdateOnVector3(Vector3Variable _v) => targetValue = _v.Value.magnitude;

        public override void UpdateValueLerp() => myComponent.value = Mathf.Lerp(myComponent.value, inverted ? myComponent.maxValue - targetValue : targetValue, Time.fixedDeltaTime / smooth);
        public override void UpdateOnEvent() => myComponent.value = inverted ? myComponent.maxValue - targetValue : targetValue;
    }
}
