#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    public class LerpChangeableVariableStateBehavior : StateMachineBehaviour
    {
        [SerializeField] private ChangeableVariable input;

        [SerializeField] private bool isRounded;

        [InfoBox("Values Set based on lerp between normalizedTime range", InfoMessageType.None)]
        [SerializeField] private FloatReference minValue;
        [SerializeField] private FloatReference maxValue = new FloatReference(1);
        [SerializeField] private FloatReference minNormalizedTime;
        [SerializeField] private FloatReference maxNormalizetTime = new FloatReference(1);

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (input == null) return;
            SetValue(CheckRounded(minValue.Value));
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (input == null) return;
            float _normalizedTime = Mathf.Clamp01(Mathf.InverseLerp(minNormalizedTime.Value, maxNormalizetTime.Value, stateInfo.normalizedTime));
            SetValue(CheckRounded(Mathf.Lerp(minValue.Value, maxValue.Value, _normalizedTime)));
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (input == null) return;
            SetValue(CheckRounded(maxValue.Value));
        }

        private float CheckRounded(float _v) => isRounded ? Mathf.Round(_v) : _v;

        private void SetValue(float _v)
        {
            switch (input)
            {
                case FloatVariable _f:
                    _f.SetValue(_v);
                    break;
                case IntVariable _i:
                    _i.SetValue(Mathf.RoundToInt(_v));
                    break;
                default:
                    break;
            }
        }
    }
}