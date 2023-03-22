using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Float")]
    [System.Serializable]
    public class FloatVariable : BaseVariable<float>
    {

        public void ApplyChange(float amount)
        {
            currentValue += amount;
            if (amount != 0)
                OnChange();
        }
        public void ApplyChange(FloatVariable amount) => ApplyChange(amount.Value);
        public void ApplyChange(ChangeableVariable amount) => ApplyChange(amount switch { IntVariable _i => _i.Value, _ => 0f });


        public void MultiplyBy(float amount)
        {
            currentValue *= amount;
            if (amount == 0)
                return;
            OnChange();
        }
        public void MultiplyBy(FloatVariable amount) => MultiplyBy(amount.Value);
        public void MultiplyBy(ChangeableVariable amount) => MultiplyBy(amount switch { IVariableValueTo _i => _i.ValueToFloat(), _ => 0f });

        public void SetRandom(float max)
        {
            currentValue = Random.Range(0, max);
            OnChange();
        }


        public override float ValueToFloat() => Value;
        public override int ValueToInt() => Mathf.RoundToInt(Value);
        public override bool ValueToBool() => Value > 0;
        public override string ValueToString(string format = "") => Value.ToString(format);


        public override void StringToValue(string _v) => SetValue(_v.Length);
        public override void IntToValue(int _v) => SetValue(_v);
        public override void FloatToValue(float _v) => SetValue(_v);
        public override void BoolToValue(bool _v) => SetValue(_v ? 1f : 0f);

    }
}
