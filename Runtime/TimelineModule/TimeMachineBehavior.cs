using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Arkham.Onigiri.Variables;
using Arkham.Onigiri.Events;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class TimeMachineBehavior : PlayableBehaviour
{
    [Title("lol")]
    [SerializeField] private float timeToJump;

    [SerializeField] private bool goOutAtCondition = false;
    [SerializeField] private GameEvent gameEvent;
    [SerializeField, HideIf("gameEvent")] private ChangeableVariable variable;

    [ShowIfGroup("IsFloat")]
    [BoxGroup("IsFloat/Float Condition")]
    [SerializeField] private CheckConditions floatCondition;
    [BoxGroup("IsFloat/Float Condition")]
    [SerializeField] private float floatValue;

    [ShowIfGroup("IsInt")]
    [BoxGroup("IsInt/Int Condition")]
    [SerializeField] private CheckConditions intCondition;
    [BoxGroup("IsInt/Int Condition")]
    [SerializeField] private int intValue;

    [ShowIfGroup("IsBool")]
    [BoxGroup("IsBool/Bool Condition")]
    [ValueDropdown("ValuesFunction")]
    [SerializeField] private CheckConditions boolCondition;

    [HideInInspector] private bool isPlaying = false;
    [HideInInspector] private bool processGameEvent = false;
    [HideInInspector] public double startTime = 0;
    [HideInInspector] public double endTime = 0;

    // BEHAVIOR
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (processGameEvent)
        {
            processGameEvent = false;
            isPlaying = false;
            playable.GetGraph().GetRootPlayable(0).SetTime(endTime);
            return;
        }

        if (gameEvent != null) return;

        if (!goOutAtCondition) return;
        if (!CheckCondition()) return;
        playable.GetGraph().GetRootPlayable(0).SetTime(endTime);
        isPlaying = false;
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        isPlaying = true;
        processGameEvent = false;
        gameEvent?.RegisterDelegate(OnGameEventRaise);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (!isPlaying) return;
        if (CheckCondition()) return;

        playable.GetGraph().GetRootPlayable(0).SetTime(startTime);

        isPlaying = false;
    }


    //  METHODS
    private bool CheckCondition()
    {
        switch (variable)
        {
            case FloatVariable _f:
                switch (floatCondition)
                {
                    case CheckConditions.equal:
                        return _f.Value == floatValue;
                    case CheckConditions.less:
                        return _f.Value < floatValue;
                    case CheckConditions.more:
                        return _f.Value > floatValue;
                    case CheckConditions.moreEqual:
                        return _f.Value >= floatValue;
                    case CheckConditions.lessEqual:
                        return _f.Value <= floatValue;
                    case CheckConditions.different:
                        return _f.Value != floatValue;
                    default:
                        return false;
                }
            case IntVariable _i:
                switch (intCondition)
                {
                    case CheckConditions.equal:
                        return _i.Value == intValue;
                    case CheckConditions.less:
                        return _i.Value < intValue;
                    case CheckConditions.more:
                        return _i.Value > intValue;
                    case CheckConditions.moreEqual:
                        return _i.Value >= intValue;
                    case CheckConditions.lessEqual:
                        return _i.Value <= intValue;
                    case CheckConditions.different:
                        return _i.Value != intValue;
                    default:
                        return false;
                }
            case BoolVariable _b:
                switch (boolCondition)
                {
                    case CheckConditions.equal:
                        return _b.Value;
                    case CheckConditions.different:
                        return !_b.Value;
                    default:
                        return false;
                }
            default:
                return false;
        }
    }


    //  EVENTS
    private void OnGameEventRaise()
    {
        gameEvent?.UnRegisterDelegate(OnGameEventRaise);
        processGameEvent = true;
    }


    //  ODIN
    private bool IsFloat() => variable != null && variable.GetType() == typeof(FloatVariable);
    private bool IsInt() => variable != null && variable.GetType() == typeof(IntVariable);
    private bool IsBool() => variable != null && variable.GetType() == typeof(BoolVariable);

    public enum CheckConditions { equal, less, more, moreEqual, lessEqual, different }
    private IList<ValueDropdownItem<CheckConditions>> ValuesFunction()
    {
        return new ValueDropdownList<CheckConditions>
        {
            { "isTrue",   CheckConditions.equal },
            { "isFalse",  CheckConditions.different }
        };
    }
}
