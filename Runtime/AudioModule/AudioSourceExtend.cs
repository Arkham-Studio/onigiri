#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AudioModule
{
    public class AudioSourceExtend : MonoBehaviour
    {
        [SerializeField] private AudioSource myAudioSource;
        [SerializeField] private AudioClipVariable audioClipToPlay;
        [SerializeField] private FloatReference timeDelay;

        [SerializeField] private bool checkEndedEvent = true;
        private bool isEnded = true;

        [ShowIf("checkEndedEvent")]
        public UnityEvent onAudioSourceEnd;

        //  MONOS
        void Start()
        {
            if (myAudioSource == null) myAudioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (audioClipToPlay != null) audioClipToPlay.onChange.AddListener(OnAudioClipChange);
        }

        private void OnDisable()
        {
            if (audioClipToPlay != null) audioClipToPlay.onChange.RemoveListener(OnAudioClipChange);
        }
        private void FixedUpdate()
        {
            if (!checkEndedEvent) return;
            if (myAudioSource.clip == null) return;
            if (isEnded) return;

            float progress = Mathf.Clamp01(myAudioSource.time / myAudioSource.clip.length);

            if (progress >= 0.98f)
            {
                isEnded = true;
                onAudioSourceEnd.Invoke();
            }

        }


        //  EVENTS
        private void OnAudioClipChange()
        {
            if (myAudioSource == null) return;
            if (audioClipToPlay.Value == null)
            {
                myAudioSource.Stop();
                return;
            }

            isEnded = false;

            myAudioSource.clip = audioClipToPlay.Value;
            if (timeDelay != null) myAudioSource.time = timeDelay.Value;
            myAudioSource.Play();


        }


    }
}
