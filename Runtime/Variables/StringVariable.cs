#pragma warning disable CS0649
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/String")]
    public class StringVariable : BaseVariable<string>
    {
        public override int ValueToInt() => int.TryParse(Value, out int _r) ? _r : 0;
        public override string ValueToString() => Value;


        [SerializeField] private string decimalFormat = "0.00";
        [SerializeField, TextArea(4, 8)] private string textZone;


        private void OnValidate()
        {
            DefaultValue = textZone;
            SetValueQuiet(textZone);
        }


        public void FloatToText(float _f) => SetValue(_f.ToString(decimalFormat));
        public void IntToText(int _i) => SetValue(_i.ToString());
        public void FloatVariableToText(FloatVariable _f) => SetValue(_f.Value.ToString(decimalFormat));
        public void IntVariableToText(IntVariable _i) => SetValue(_i.Value.ToString());
        public void FromStringVariable(StringVariable _s) => SetValue(_s.Value);


        public override void StringToValue(string _v) => SetValue(_v);
        public override void IntToValue(int _v) => SetValue(_v.ToString());
        public override void FloatToValue(float _v) => SetValue(_v.ToString());
        public override void BoolToValue(bool _v) => SetValue(_v.ToString());
    }
}
