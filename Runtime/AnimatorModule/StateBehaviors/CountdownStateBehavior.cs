using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    public class CountdownStateBehavior : StateMachineBehaviour
    {
        [Title("Set Variable to State NormalizedTime")]
        public FloatVariable countdown;
        public bool isRounded = true;
        public bool isReverse = false;
        [ShowIf("isReverse")]
        public float startAt = 10;

        private float value;


        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            value = stateInfo.normalizedTime * stateInfo.length;
            value = isReverse ? startAt - value : value;
            value = isRounded ? Mathf.Round(value) : value;

            countdown.SetValue(value);

        }

    }
}
