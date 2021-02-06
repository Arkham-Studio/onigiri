using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    public class TriggerListenerStateBehavior : StateMachineBehaviour
    {
        [SerializeField] private StringVariable actualTrigger;

        private UnityAction handler;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            handler = () => { OnChange(animator, layerIndex); };
            actualTrigger.onChange.AddListener(handler);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            actualTrigger.onChange.RemoveListener(handler);
        }

        private void OnChange(Animator _animator, int _layer)
        {
            _animator.SetTrigger(actualTrigger.Value);
        }
    }
}
