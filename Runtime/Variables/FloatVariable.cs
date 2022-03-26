using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Float")]
    [System.Serializable]
    public class FloatVariable : BaseVariable<float>
    {
        public void ApplyChange(float amount)
        {
            Value += amount;
            if (amount != 0)
                OnChange();
        }

        public void ApplyChange(FloatVariable amount)
        {
            Value += amount.Value;
            if (amount.Value != 0)
                OnChange();
        }

        public void MultiplyBy(float amount)
        {
            Value *= amount;
            if (amount != 1)
                OnChange();
        }

        public void SetRandom(float max)
        {
            Value = Random.Range(0, max);
            OnChange();
        }
    }
}
