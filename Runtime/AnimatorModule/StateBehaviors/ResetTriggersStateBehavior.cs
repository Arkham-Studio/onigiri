using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    public class ResetTriggersStateBehavior : StateMachineBehaviour
    {
        public string[] triggersName;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var item in triggersName)
                animator.ResetTrigger(Animator.StringToHash(item));
        }
    }
}
