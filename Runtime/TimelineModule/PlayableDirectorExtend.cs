using Sirenix.OdinInspector;
using System;
using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Arkham.Onigiri.Timeline
{
    public class PlayableDirectorExtend : MonoBehaviour
    {
        [Title("REFERENCES")]
        [SerializeField] private PlayableDirector myPlayableDirector;

        [Title("VARIABLES")]
        [Tooltip("if not null, on change set time to variable value")]
        [SerializeField] private FloatVariable playableTimeSetter;
        [Tooltip("if not null, on change set actual Timeline with variable value")]
        [SerializeField] private PlayableAssetVariable playableVariable;
        [Tooltip("if not null, on change search a marker named with this variable value and set time to")]
        [SerializeField] private DenumVariable jumpToMarkerNamed;

        //  EVENTS
        [PropertyOrder(2), ShowIf("IsPlayEvent")]
        [SerializeField] private PlayableDirectorUnityEvent onDirectorPlay;
        [PropertyOrder(2), ShowIf("IsPauseEvent")]
        [SerializeField] private PlayableDirectorUnityEvent onDirectorPause;
        [PropertyOrder(2), ShowIf("IsStopEvent")]
        [SerializeField] private PlayableDirectorUnityEvent onDirectorStop;


        //  MONOS
        private void OnEnable()
        {
            myPlayableDirector = myPlayableDirector ?? GetComponent<PlayableDirector>();

            // time
            playableTimeSetter?.onChange.AddListener(OnForcedTimeChange);

            // playable
            playableVariable?.onChange.AddListener(OnPlayableChange);

            //  markers
            jumpToMarkerNamed?.onChange.AddListener(OnJumpToMarkerNamedChange);

            // events
            myPlayableDirector.played += OnDirectorPlay;
            myPlayableDirector.paused += OnDirectorPause;
            myPlayableDirector.stopped += OnDirectorStop;
        }

        private void OnDisable()
        {
            // time
            playableTimeSetter?.onChange.RemoveListener(OnForcedTimeChange);

            //  events
            myPlayableDirector.played -= OnDirectorPlay;
            myPlayableDirector.paused -= OnDirectorPause;
            myPlayableDirector.stopped -= OnDirectorStop;
        }


        //  METHODS
        [Button(ButtonSizes.Large), Title("METHODS")]
        public void GoToNextMarkerNamed(string _markerName)
        {
            foreach (Marker item in ((TimelineAsset)myPlayableDirector.playableAsset).markerTrack.GetMarkers())
            {
                if (!item.name.Equals(_markerName)) continue;
                myPlayableDirector.time = item.time;
                return;
            }
        }

        [Button(ButtonSizes.Large)]
        public void GoToTimachineClipNamed(string _clipName)
        {
            foreach (TrackAsset item in ((TimelineAsset)myPlayableDirector.playableAsset).GetRootTracks())
            {
                if (item is TimeMachineTrack _track)
                {
                    foreach (TimelineClip _item in _track.GetClips())
                    {
                        if (!_item.displayName.Equals(_clipName)) continue;
                        myPlayableDirector.time = item.start;
                        return;

                    }
                }
            }
        }



        //  EVENTS
        private void OnDirectorPlay(PlayableDirector obj) { if (IsPlayEvent) onDirectorPlay.Invoke(obj); }
        private void OnDirectorPause(PlayableDirector obj) { if (IsPauseEvent) onDirectorPause.Invoke(obj); }
        private void OnDirectorStop(PlayableDirector obj) { if (IsStopEvent) onDirectorStop.Invoke(obj); }
        private void OnForcedTimeChange() => myPlayableDirector.time = playableTimeSetter.Value;
        private void OnPlayableChange() => myPlayableDirector.playableAsset = playableVariable.Value;
        private void OnJumpToMarkerNamedChange() => GoToNextMarkerNamed(jumpToMarkerNamed.Value.name);


        //  ODIN
        [Title("EVENTS"), SerializeField, EnumToggleButtons, PropertyOrder(1)] private EventsType activeEvents;
        bool IsPlayEvent => ((int)activeEvents & (int)EventsType.Play) != 0;
        bool IsPauseEvent => ((int)activeEvents & (int)EventsType.Pause) != 0;
        bool IsStopEvent => ((int)activeEvents & (int)EventsType.Stop) != 0;

        [Flags]
        public enum EventsType
        {
            Play = 1 << 0,
            Pause = 1 << 1,
            Stop = 1 << 2,
            All = Play | Pause | Stop
        }
    }
}
