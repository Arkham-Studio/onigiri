#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using Arkham.Onigiri.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;

namespace Arkham.Onigiri.AppStates
{
    [RequireComponent(typeof(Animator))]
    public class AppStateAnimatorController : MonoBehaviour
    {
        [Title("REFERENCES")]
        [SerializeField] private Animator myAnimator;

        [Title("STATES", "DenumVariable onChange play Animator State with same Denum name")]
        [SerializeField] private DenumVariable actualState;
        [SerializeField] private DenumVariable setState;

        [SerializeField] private Denum[] allDenumStates = new Denum[0];
        private Dictionary<int, Denum> cachedDenumStatesList = new Dictionary<int, Denum>();


        private void OnValidate()
        {
            myAnimator = myAnimator != null ? myAnimator : GetComponent<Animator>();
        }

        private void OnEnable()
        {
            myAnimator = myAnimator != null ? myAnimator : GetComponent<Animator>();

            if (setState != null)
            {
                setState.onChange.AddListener(PlayDenumState);
                PlayDenumState();
            }


            for (int i = 0; i < allDenumStates.Length; i++)
                cachedDenumStatesList.Add(Animator.StringToHash(allDenumStates[i].name), allDenumStates[i]);
        }

        private void OnDisable()
        {
            setState?.onChange.RemoveListener(PlayDenumState);

        }

        public void PlayDenumState()
        {
            if (setState.Value == null) return;
            myAnimator.Play(setState.Value.name, 0, 0f);
        }

        public void NotifyStateChange(int _stateHash)
        {
            if (actualState == null) return;
            if (!cachedDenumStatesList.ContainsKey(_stateHash)) return;
            actualState.SetValue(cachedDenumStatesList[_stateHash]);
        }

    }
}