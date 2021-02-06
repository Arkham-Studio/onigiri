using UnityEngine;
using UnityEngine.Events;

public class IfBoolEvent : IfEventBase<bool, BoolVariable>
{
    public override bool Test(bool _v) => _v == test;
}
