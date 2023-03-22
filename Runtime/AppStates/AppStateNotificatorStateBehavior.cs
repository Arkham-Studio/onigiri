using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Arkham.Onigiri.AppStates
{
    [InfoBox("Automaticaly send state name OnStateEnter to the AppStateAnimatorController on gameobject", InfoMessageType.None)]
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
