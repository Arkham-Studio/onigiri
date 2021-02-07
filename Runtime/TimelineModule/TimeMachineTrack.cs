using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Arkham.Onigiri.TimelineModule
{
    [TrackClipType(typeof(TimeMachineAsset))]
    public class TimeMachineTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var clip in GetClips())
            {
                if (clip.asset is TimeMachineAsset _asset)
                {
                    _asset.template.startTime = clip.start;
                    _asset.template.endTime = clip.end;
                }
            }

            return base.CreateTrackMixer(graph, go, inputCount);
        }
    }
}