using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Long")]
    public class LongVariable : BaseVariable<long>
    {

        public override float ValueToFloat() => Value;
        public override int ValueToInt() => Mathf.RoundToInt(Value);
        public override bool ValueToBool() => Value > 0;


        public override void StringToValue(string _v) => SetValue(_v.Length);
        public override void IntToValue(int _v) => SetValue(_v);
        public override void FloatToValue(float _v) => SetValue((long)_v);
        public override void BoolToValue(bool _v) => SetValue(_v ? 1 : 0);
    }
}
