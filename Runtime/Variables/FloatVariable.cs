using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Float")]
    [System.Serializable]
    public class FloatVariable : BaseVariable<float>
    {
        public override float ValueToFloat() => Value;
        public override int ValueToInt() => Mathf.RoundToInt(Value);
        public override bool ValueToBool() => Value > 0;

        public void ApplyChange(float amount)
        {
            currentValue += amount;
            if (amount != 0)
                OnChange();
        }

        public void ApplyChange(FloatVariable amount)
        {
            currentValue += amount.Value;
            if (amount.Value != 0)
                OnChange();
        }

        public void MultiplyBy(float amount)
        {
            currentValue *= amount;
            if (amount != 1)
                OnChange();
        }

        public void SetRandom(float max)
        {
            currentValue = Random.Range(0, max);
            OnChange();
        }
    }
}
