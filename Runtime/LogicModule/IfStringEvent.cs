using Arkham.Onigiri.Variables;

namespace Arkham.Onigiri.LogicModule
{
    public class IfStringEvent : IfEventBase<string, StringVariable>
    {
        public override bool Test(string _v) => _v.Equals(test);
    }
}
