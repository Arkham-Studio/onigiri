using System.Collections;
using System.Collections.Generic;
using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class ChangeableVariableBehavior : PlayableBehaviour
{

    [SerializeField] private ChangeableVariable variable;
    [SerializeField] private float value;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        

        if (variable == null) return;

        switch (variable)
        {
            case FloatVariable _f:
                _f.SetValue(value);
                break;
            case IntVariable _i:
                _i.SetValue(Mathf.RoundToInt(value));
                break;
            default:
                break;
        }
    }

}
