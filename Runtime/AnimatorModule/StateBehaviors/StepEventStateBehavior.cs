#pragma warning disable CS0649
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    public class StepEventStateBehavior : StateMachineBehaviour
    {

        [SerializeField, MinValue(0)] private float normalizedTime;
        public UnityEvent stepEvent;

        private bool triggered = false;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            triggered = false;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (triggered) return;
            if (stateInfo.normalizedTime < normalizedTime) return;
            triggered = true;
            stepEvent.Invoke();
        }

    }
}
