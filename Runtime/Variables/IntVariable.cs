using Arkham.Onigiri.Attributes;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Int")]
    public class IntVariable : BaseVariable<int>
    {


        public void ApplyChange(int amount)
        {
            currentValue += amount;
            if (amount != 0)
                OnChange();
        }

        public void ApplyChange(IntVariable amount)
        {
            currentValue += amount.Value;
            if (amount.Value != 0)
                OnChange();
        }

        public void MultiplyBy(float mult)
        {
            currentValue = Mathf.RoundToInt(Value * mult);
            if (mult != 1) OnChange();
        }

        public void SetRandom(int max)
        {
            currentValue = Random.Range(0, max);
            OnChange();
        }

        public override string ValueToString(string format = "0") => Value.ToString(format);
        public override int ValueToInt() => Value;
        public override float ValueToFloat() => Value * 1f;
        public override bool ValueToBool() => Value > 0;


        public override void StringToValue(string _v) => SetValue(_v.Length);
        public override void IntToValue(int _v) => SetValue(_v);
        public override void FloatToValue(float _v) => SetValue(Mathf.RoundToInt(_v));
        public override void BoolToValue(bool _v) => SetValue(_v ? 1 : 0);

    }
}
