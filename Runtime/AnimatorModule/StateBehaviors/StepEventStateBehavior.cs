#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    public class StepEventStateBehavior : StateMachineBehaviour
    {

        public StepPack[] stepEvents;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StepPack stepPack in stepEvents)
            {
                if (stepPack.resetOnStateEnter)
                    stepPack.triggered = false;
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StepPack stepPack in stepEvents)
            {
                if (stepPack.triggered || (stateInfo.normalizedTime < stepPack.normalizedTime.Value))
                    continue;
                stepPack.triggered = true;
                stepPack.stepEvent.Invoke();
            }
        }

        [Serializable]
        public class StepPack
        {
            [HorizontalGroup("params")] public FloatReference normalizedTime = new FloatReference(0);
            [HideInInspector] public bool resetOnStateEnter = true;
            public UnityEvent stepEvent;
            [HideInInspector] public bool triggered = false;

            [HorizontalGroup("params"), Button(ButtonSizes.Small, Name = "@resetOnStateEnter ? \"Always\" : \"Once\""), PropertyOrder(Order = -2), GUIColor("@resetOnStateEnter ? Color.white : Color.grey")]
            void ToggleCurve() => resetOnStateEnter = !resetOnStateEnter;

        }

    }
}
