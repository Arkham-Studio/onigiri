#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Playables;

namespace Arkham.Onigiri.TimelineModule
{
    [System.Serializable]
    public class ChangeableVariableBehavior : PlayableBehaviour
    {

    [SerializeField] private ChangeableVariable variable;
    [SerializeField] private float value;
    [SerializeField] private Vector3 value3;

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
            case Vector3Variable _v3:
                _v3.SetValue(value3);
                break;
            default:
                break;
        }
    }

    }
}
