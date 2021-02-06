using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeMachineAsset : PlayableAsset
{

    public TimeMachineBehavior template;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var _playable = ScriptPlayable<TimeMachineBehavior>.Create(graph, template);
        return _playable;
    }
}
