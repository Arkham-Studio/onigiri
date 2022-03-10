using Arkham.Onigiri.Variables;

namespace Arkham.Onigiri.LogicModule
{
    public class IfBoolVariable : BaseIfChangeableVariable<bool, BoolVariable, BoolReference>
    {
        public override bool Test(bool _v, ComparePack _pack)
        {
            switch (_pack.how)
            {
                case CompareOperation.equals:
                    return _pack.toThat == _v;
                case CompareOperation.different:
                    return _pack.toThat != _v;
                default:
                    return _pack.toThat == _v;
            }
        }
    }
}
