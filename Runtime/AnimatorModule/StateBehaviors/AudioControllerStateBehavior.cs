using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    public class AudioControllerStateBehavior : StateMachineBehaviour
    {
        [Tooltip("")]
        public AudioClipVariable audioClipVariable;
        public AudioClip clip;
        public string stateSpeedMultiplierName;
        public bool isChangingStateLength = true;
        [Tooltip("if true and no audioclipvariable, find first audiosource in hierarchie"), HideIf("audioClipVariable")]
        public bool isAutoGetAudioSource = false;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (isChangingStateLength)
                animator.SetFloat(stateSpeedMultiplierName, 1f / clip.length);

            if (isAutoGetAudioSource/* && audioClipVariable == null*/)
            {
                var source = animator.GetComponentInChildren<AudioSource>();
                if (source == null) return;
                source.loop = false;
                source.clip = clip;
                source.Play();
            }
            else
            {
                if (audioClipVariable == null) return;
                audioClipVariable.SetValue(clip);
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
