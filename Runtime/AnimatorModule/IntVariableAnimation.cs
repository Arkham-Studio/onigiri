#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    public class IntVariableAnimation : MonoBehaviour
    {
        public int intValue;
        private int lastIntValue;

        [SerializeField] private IntVariable intVariable;

        private void Update()
        {
            if (intValue != lastIntValue)
                intVariable.SetValue(lastIntValue = intValue);
        }
    }
}
