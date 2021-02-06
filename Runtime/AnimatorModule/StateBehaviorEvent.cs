using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    public class StateBehaviorEvent : StateMachineBehaviour
    {
        public UnityEvent onStateEnter, onStateUpdate, onStateAnimLoop, onStateExit;

        private bool haveLooped = false;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            onStateEnter.Invoke();
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            onStateUpdate.Invoke();

            float _time = stateInfo.normalizedTime % 1;

            if (haveLooped && _time < 0.99f)
            {
                haveLooped = false;
            }

            if (!haveLooped && _time > 0.98f)
            {
                haveLooped = true;
                onStateAnimLoop.Invoke();
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            onStateExit.Invoke();
        }

    }
}
