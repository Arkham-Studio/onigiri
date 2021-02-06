using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ChangeableVariableAsset : PlayableAsset
{
    [SerializeField] private ChangeableVariableBehavior template;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var _playable = ScriptPlayable<ChangeableVariableBehavior>.Create(graph, template);
        return _playable;
    }

}
