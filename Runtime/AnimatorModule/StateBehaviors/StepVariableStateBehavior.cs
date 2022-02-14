#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    public class StepVariableStateBehavior : StateMachineBehaviour
    {
        [SerializeField] private ChangeableVariable variable;
        [SerializeField, MinValue(0)] private float normalizedTime;

        [SerializeField, ShowIf("isFloat")] private float floatValue;
        [SerializeField, ShowIf("isInt")] private int intValue;
        [SerializeField, ShowIf("isString")] private string stringValue;

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
            switch (variable)
            {
                case FloatVariable f:
                    f.SetValue(floatValue);
                    break;
                case StringVariable s:
                    s.SetValue(stringValue);
                    break;
                case IntVariable i:
                    i.SetValue(intValue);
                    break;
                default:
                    break;
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{

        //}


        private bool isFloat() => variable?.GetType() == typeof(FloatVariable);
        private bool isInt() => variable?.GetType() == typeof(IntVariable);
        private bool isString() => variable?.GetType() == typeof(StringVariable);
    }
}