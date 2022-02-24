using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Arkham.Onigiri.AppStates
{
    public class AppStateNotificatorStateBehavior : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AppStateAnimatorController _controller = animator.GetComponent<AppStateAnimatorController>();
            if (_controller == null) return;

            _controller.NotifyStateChange(stateInfo.shortNameHash);
        }
    }

}
