#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.AppStates
{
    [RequireComponent(typeof(Animator))]
    public class AppStateAnimatorController : MonoBehaviour
    {
        [Title("REFERENCES")]
        [SerializeField] private Animator myAnimator;

        [Title("STATES", "DenumVariable onChange play Animator State with same Denum name")]
        [SerializeField] private DenumVariable denumState;

        private void OnEnable()
        {
            myAnimator = myAnimator != null ? myAnimator : GetComponent<Animator>();

            if (denumState != null)
            {
                denumState.onChange.AddListener(PlayDenumState);
                PlayDenumState();
            }
        }

        private void OnDisable()
        {
            denumState?.onChange.RemoveListener(PlayDenumState);

        }

        public void PlayDenumState() => myAnimator.Play(denumState.Value.name, 0, 0f);

    }
}