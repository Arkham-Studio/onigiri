using UnityEngine;
using UnityEngine.Playables;
#pragma warning disable CS0649
namespace Arkham.Onigiri.TimelineModule
{
    public class ChangeableVariableAsset : PlayableAsset
    {
        [SerializeField] private ChangeableVariableBehavior template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var _playable = ScriptPlayable<ChangeableVariableBehavior>.Create(graph, template);
            return _playable;
        }

    }
}
