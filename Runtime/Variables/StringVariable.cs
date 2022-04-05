#pragma warning disable CS0649
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/String")]
    public class StringVariable : BaseVariable<string>
    {

        [SerializeField] private string decimalFormat = "0.00";
#if UNITY_EDITOR

        [SerializeField, TextArea(4, 8)] private string textZone;


        private void OnValidate()
        {
            DefaultValue = textZone;
            SetValueQuiet(textZone);
        }
#endif

        public override int ValueToInt() => int.TryParse(Value, out int _r) ? _r : 0;
        public override float ValueToFloat() => float.TryParse(Value, out float _r) ? _r : 0f;
        public override string ValueToString() => Value;


        public void FloatToText(float _f) => SetValue(_f.ToString(decimalFormat));
        public void IntToText(int _i) => SetValue(_i.ToString());
        public void FloatVariableToText(FloatVariable _f) => SetValue(_f.Value.ToString(decimalFormat));
        public void IntVariableToText(IntVariable _i) => SetValue(_i.Value.ToString());
        public void FromStringVariable(StringVariable _s) => SetValue(_s.Value);


        public override void StringToValue(string _v) => SetValue(_v);
        public override void IntToValue(int _v) => SetValue(_v.ToString());
        public override void FloatToValue(float _v) => SetValue(_v.ToString());
        public override void BoolToValue(bool _v) => SetValue(_v.ToString());


        public static implicit operator int(StringVariable reference) => int.TryParse(reference.Value, out int _r) ? _r : 0;
        public static implicit operator float(StringVariable reference) => float.TryParse(reference.Value, out float _r) ? _r : 0f;
        public static implicit operator bool(StringVariable reference) => reference.Value.Length > 0;
    }
}
