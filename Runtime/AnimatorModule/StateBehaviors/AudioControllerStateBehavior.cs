using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    public class AudioControllerStateBehavior : StateMachineBehaviour
    {
        [EnumToggleButtons, HideLabel]
        public BehaviorType behaviorType;

        public AudioClipVariable audioClipVariable;
        [Tooltip("if true and no audioclipvariable, find first audiosource in hierarchie"), HideIf("audioClipVariable")]
        public bool autoGetAudioSource = false;
        [ShowIf("behaviorType", BehaviorType.SetClip)]
        public AudioClip clip;

        public bool changeStateLength = true;
        [ShowIf("changeStateLength")]
        public string stateSpeedMultiplierName;


        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (changeStateLength && clip != null && behaviorType == BehaviorType.SetClip)
                animator.SetFloat(stateSpeedMultiplierName, 1f / clip.length);
            else if (changeStateLength && audioClipVariable != null && behaviorType == BehaviorType.PlayVariableClip)
                animator.SetFloat(stateSpeedMultiplierName, 1f / audioClipVariable.Value.length);

            if (autoGetAudioSource/* && audioClipVariable == null*/)
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
                if (behaviorType == BehaviorType.SetClip)
                    audioClipVariable.SetValue(clip);
                else if (behaviorType == BehaviorType.PlayVariableClip)
                    audioClipVariable.OnChange();
            }
        }

        // ODIN
        public enum BehaviorType { SetClip, PlayVariableClip }
    }
}
