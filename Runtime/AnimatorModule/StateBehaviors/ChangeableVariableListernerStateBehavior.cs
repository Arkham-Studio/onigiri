using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    public class ChangeableVariableListernerStateBehavior : StateMachineBehaviour
    {

        public ChangeableVariable variable;
        public UnityEvent onVariableChange;

        private UnityAction handler;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            handler = () => {
                OnVariableChange();
                animator.SetTrigger(variable.name);
            };
            variable.onChange.AddListener(handler);
        }

      
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            variable.onChange.RemoveListener(handler);
            handler = null;
        }

        public void OnVariableChange()
        {
            onVariableChange.Invoke();
        }

    }
}
