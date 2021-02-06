using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    public class StateListenerStateBehavior : StateMachineBehaviour
    {

        [SerializeField] private StringVariable actualState;

        private UnityAction handler;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            handler = () => { OnChange(animator, layerIndex); };
            actualState.onChange.AddListener(handler);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            actualState.onChange.RemoveListener(handler);
        }

        private void OnChange(Animator _animator, int _layer)
        {
            _animator.Play(actualState.Value, _layer);
        }
    }
}
