using Arkham.Onigiri.Variables;

namespace Arkham.Onigiri.LogicModule
{
    public class IfBoolEvent : IfEventBase<bool, BoolVariable>
    {
        public override bool Test(bool _v) => _v == test;
    }
}
