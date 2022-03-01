#pragma warning disable CS0649
using System;
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace Arkham.Onigiri.AudioModule
{
    [CreateAssetMenu(menuName = "Managers/Audio Mixer Manager")]
    public class AudioMixerManager : ScriptableObject
    {
        [Title("REFERENCES")]
        [SerializeField] private AudioMixer myMixer;

        [Title("SNAPSHOTS")]
        [SerializeField] private AudioSnapshotVariable actualSnapshot;
        [SerializeField] private DenumVariable snapshotState;
        [SerializeField] private float snapshotTransitionTime;

        [Title("PARAMETERS")]
        private int fadeClipPackIndex = -1;
        [SerializeField] private FadeClipPack[] fadeClipPacks;
        [SerializeField] private ParameterDBPack[] mixerParametersDB;


        //  MONOS
        public void OnEnable()
        {
            fadeClipPackIndex = -1;

            actualSnapshot?.onChange.AddListener(OnSnapshotChange);
            snapshotState?.onChange.AddListener(OnSnapshotStateChange);

            if (mixerParametersDB != null)
                foreach (ParameterDBPack item in mixerParametersDB) item?.Bind(myMixer);

            if (actualSnapshot != null && actualSnapshot.Value != null)
                OnSnapshotChange();
        }

        private void OnDisable()
        {
            actualSnapshot?.onChange.RemoveListener(OnSnapshotChange);
            snapshotState?.onChange.RemoveListener(OnSnapshotStateChange);

            foreach (ParameterDBPack item in mixerParametersDB) item?.UnBind();
        }


        //  METHODS
        public void ChangeDBParameter(ChangeableVariable _variable)
        {
            switch (_variable)
            {
                case FloatVariable _f:
                    myMixer?.SetFloat(_variable.name, Mathf.Log10(_f.Value) * 20f);
                    break;
                case IntVariable _i:
                    myMixer?.SetFloat(_variable.name, Mathf.Log10(_i.Value * 1f) * 20f);
                    break;
                case BoolVariable _b:
                    myMixer?.SetFloat(_variable.name, Mathf.Log10(_b ? 0.999f : 0.0001f) * 20f);
                    break;
            }
        }

        public void FadeToThisClip(AudioClip _audioClip)
        {
            fadeClipPackIndex = (fadeClipPackIndex + 1) % fadeClipPacks.Length;
            fadeClipPacks[fadeClipPackIndex].variable.SetValue(_audioClip);
            fadeClipPacks[fadeClipPackIndex].snapshot.TransitionTo(snapshotTransitionTime);
        }

        public void FadeToNothing()
        {
            fadeClipPackIndex = (fadeClipPackIndex + 1) % fadeClipPacks.Length;
            fadeClipPacks[fadeClipPackIndex].variable.SetToNull();
            fadeClipPacks[fadeClipPackIndex].snapshot.TransitionTo(snapshotTransitionTime);
        }

        public void ChangeSnapshotTransitionTime(float _newTime) => snapshotTransitionTime = _newTime;

        //  EVENTS
        private void OnSnapshotChange() => actualSnapshot?.Value?.TransitionTo(snapshotTransitionTime);

        public void OnSnapshotStateChange()
        {
            var _snapshot = myMixer.FindSnapshot(snapshotState.Value.name);
            if (_snapshot != null) actualSnapshot.SetValue(_snapshot);
        }


        //  UTILS
        [Serializable]
        public class FadeClipPack
        {
            public AudioClipVariable variable;
            public AudioMixerSnapshot snapshot;
        }

        [Serializable]
        public class ParameterDBPack
        {
            private AudioMixer myMixer;
            public ChangeableVariable variable;


            public void OnChange()
            {
                switch (variable)
                {
                    case FloatVariable _f:
                        myMixer?.SetFloat(variable.name, Mathf.Log10(_f.Value) * 20f);
                        break;
                    case IntVariable _i:
                        myMixer?.SetFloat(variable.name, Mathf.Log10(_i.Value * 1f) * 20f);
                        break;
                    case BoolVariable _b:
                        myMixer?.SetFloat(variable.name, Mathf.Log10(_b ? 0.999f : 0.001f) * 20f);
                        break;
                    default:
                        break;
                }
            }

            public void Bind(AudioMixer _mixer)
            {
                myMixer = _mixer;
                variable?.onChange.AddListener(OnChange);
                OnChange();
            }

            public void UnBind() => variable?.onChange.RemoveListener(OnChange);
        }
    }
}
