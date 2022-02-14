#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    public class FloatVariableStateBehavior : StateMachineBehaviour
    {

        [SerializeField] private FloatVariable input;

        [SerializeField] private bool isRounded;

        [SerializeField] private FloatReference minValue;
        [SerializeField] private FloatReference maxValue;

        [SerializeField] private FloatReference minOffset;
        [SerializeField] private FloatReference maxOffset = new FloatReference(1);

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (input == null) return;
            input.SetValue(CheckRounded(minValue.Value));
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (input == null) return;
            float _normalizedTime = Mathf.Clamp01(Mathf.InverseLerp(minOffset.Value, maxOffset.Value, stateInfo.normalizedTime));
            input.SetValue(CheckRounded(Mathf.Lerp(minValue.Value, maxValue.Value, _normalizedTime)));
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (input == null) return;
            input.SetValue(CheckRounded(maxValue.Value));
        }

        private float CheckRounded(float _v) => isRounded ? Mathf.Round(_v) : _v;

    }
}
