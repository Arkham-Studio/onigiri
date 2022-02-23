#pragma warning disable CS0649
using System;
using Arkham.Onigiri.Variables;
using Arkham.Onigiri.Events;
using Sirenix.OdinInspector;
using UnityEngine.Animations;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorExtend : MonoBehaviour
    {
        [Title("REFERENCES")]
        [SerializeField] private Animator myAnimator;

        [Title("ANIMATIONS EVENTS")]
        public AnimEventPack[] animEvents;

        private string parameterName = "";

        private void OnEnable() => myAnimator = myAnimator != null ? myAnimator : GetComponent<Animator>();

        //  METHODS
        public void SkipActualState() => myAnimator.Play(myAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0.99f);

        public void PlayDenumState(DenumVariable _denum) => myAnimator.Play(_denum.Value.name, 0, 0f);

        public void SetDenumTriger(DenumVariable _denum) => myAnimator.SetTrigger(_denum.Value.name);

        public void SetUpdateMode(int _mode) => myAnimator.updateMode = (AnimatorUpdateMode)_mode;

        //  parameters
        public void SetParameterName(string v) => parameterName = v;
        public void SetBool(BoolVariable v) => myAnimator.SetBool(v.name, v.Value);
        public void SetBool(bool v) => myAnimator.SetBool(parameterName, v);
        public void SetInt(IntVariable v) => myAnimator.SetInteger(v.name, v.Value);
        public void SetInt(int v) => myAnimator.SetInteger(parameterName, v);
        public void SetFloat(FloatVariable v) => myAnimator.SetFloat(v.name, v.Value);
        public void SetFloat(float v) => myAnimator.SetFloat(parameterName, v);

        //  animation events
        public void TriggerAnimEventByIndex(int i)
        {
            if (i < 0 || i >= animEvents.Length) return;
            animEvents[i].response.Invoke();
        }

        public void TriggerAnimEventByName(string s)
        {
            if (s == "") return;
            foreach (AnimEventPack item in animEvents)
            {
                if (item.animEventName != s) continue;
                item.response.Invoke();
            }

        }


        [Serializable]
        public class AnimEventPack
        {
            public string animEventName;
            public UnityEvent response;
        }
    }
}