﻿using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    [ExecuteInEditMode]
    public class AudioControllerStateBehavior : StateMachineBehaviour
    {
        [EnumToggleButtons, HideLabel]
        public BehaviorType behaviorType;

        public AudioClipVariable destination;
        [Tooltip("if true and no audioclipvariable, find first audiosource in hierarchie"), HideIf("destination")]
        public bool autoGetAudioSource = false;
        [ShowIf("behaviorType", BehaviorType.SetClipFrom)]
        public AudioClipReference source;

        public bool changeStateLength = true;
        [ShowIf("changeStateLength")]
        public string stateSpeedMultiplierName;
        [ShowIf("changeStateLength")]
        public float addTimeToEnd = 0;

 
        private void OnEnable()
        {
            Debug.Log("ok");
        }


        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (changeStateLength && source.Value != null && behaviorType == BehaviorType.SetClipFrom)
                animator.SetFloat(stateSpeedMultiplierName, 1f / (source.Value.length + addTimeToEnd));
            else if (changeStateLength && destination != null && behaviorType == BehaviorType.PlayVariableClip)
                animator.SetFloat(stateSpeedMultiplierName, 1f / (destination.Value.length + addTimeToEnd));

            if (autoGetAudioSource && source.Value != null)
            {
                var _source = animator.GetComponentInChildren<AudioSource>();
                if (_source == null) return;
                _source.loop = false;
                _source.clip = source.Value;
                _source.Play();
            }
            else
            {
                if (destination == null) return;
                if (behaviorType == BehaviorType.SetClipFrom)
                    destination.SetValue(source.Value);
                else if (behaviorType == BehaviorType.PlayVariableClip)
                    destination.OnChange();
            }
        }

        // ODIN
        public enum BehaviorType { SetClipFrom, PlayVariableClip }
    }
}
