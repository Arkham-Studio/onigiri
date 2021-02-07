using UnityEngine;
using UnityEngine.Playables;

namespace Arkham.Onigiri.TimelineModule
{
    public class TimeMachineAsset : PlayableAsset
    {

        public TimeMachineBehavior template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var _playable = ScriptPlayable<TimeMachineBehavior>.Create(graph, template);
            return _playable;
        }
    }
}
