using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Min Max")]
    public class MinMaxVariable : BaseVariable<OnigiriMinMax>
    {
        public new float Value => currentValue.Lerp();

        public void SetValue(float _t)
        {
            currentValue.t = _t;
            OnChange();
        }
        public void SetMin(float _v)
        {
            currentValue.minValue.ConstantValue = _v;
            OnChange();
        }
        public void SetMax(float _v)
        {
            currentValue.maxValue.ConstantValue = _v;
            OnChange();
        }
        public void SetMinMax(float _min, float _max, float _t = 0)
        {
            currentValue.minValue.ConstantValue = _min;
            currentValue.maxValue.ConstantValue = _max;
            currentValue.t = _t;
            OnChange();
        }
        public void SetValueQuiet(float _t) => currentValue.t = _t;
        public void SetMinQuiet(float _v) => currentValue.minValue.ConstantValue = _v;
        public void SetMaxQuiet(float _v) => currentValue.maxValue.ConstantValue = _v;
        public void SetMinMaxQuiet(float _min, float _max, float _t = 0)
        {
            currentValue.minValue.ConstantValue = _min;
            currentValue.maxValue.ConstantValue = _max;
            currentValue.t = _t;
        }
    }

    [System.Serializable]
    public class OnigiriMinMax
    {
        [HorizontalGroup(Title ="Min / Max"), HideLabel]
        public FloatReference minValue = new FloatReference(0) { UseConstant = true};
        [HorizontalGroup(), HideLabel]
        public FloatReference maxValue = new FloatReference(1) { UseConstant = true };

        [Range(0.0f,1.0f), HideLabel]
        public float t;

        public float Lerp() => Mathf.Lerp(minValue, maxValue, t);
        public float Lerp(float _t) => Mathf.Lerp(minValue, maxValue, _t);
    }
}
