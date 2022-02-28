﻿using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Int")]
    public class IntVariable : BaseVariable<int>
    {

        public void ApplyChange(int amount)
        {
            Value += amount;
            if (amount != 0)
                OnChange();
        }

        public void ApplyChange(IntVariable amount)
        {
            Value += amount.Value;
            if (amount.Value != 0)
                OnChange();
        }

        public void MultiplyBy(float mult)
        {
            Value = Mathf.RoundToInt(Value * mult);
            if (mult != 1) OnChange();
        }

        public void SetRandom(int max)
        {
            Value = Random.Range(0, max);
            OnChange();
        }
    }
}
