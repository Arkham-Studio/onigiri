using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;

public class IfStringEvent : IfEventBase<string, StringVariable>
{
    public override bool Test(string _v) => _v.Equals(test);
}
