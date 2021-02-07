#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    public class FloatVariableAnimation : MonoBehaviour
    {
        public float floatValue;
        private float lastFloatValue;

        [SerializeField] private FloatVariable floatVariable;

        private void Update()
        {
            if (floatValue == lastFloatValue) return;
            floatVariable.SetValue(floatValue);
            lastFloatValue = floatValue;
        }
    }
}
