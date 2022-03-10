using Arkham.Onigiri.Variables;

namespace Arkham.Onigiri.LogicModule
{
    public class IfIntVariable : BaseIfChangeableVariable<int, IntVariable, IntReference>
    {
        public override bool Test(int _v, ComparePack _pack)
        {
            switch (_pack.how)
            {
                case CompareOperation.equals:
                    return _v == _pack.toThat.Value;
                case CompareOperation.less:
                    return _v < _pack.toThat.Value;
                case CompareOperation.more:
                    return _v > _pack.toThat.Value;
                case CompareOperation.lessEqual:
                    return _v <= _pack.toThat.Value;
                case CompareOperation.moreEqual:
                    return _v >= _pack.toThat.Value;
                case CompareOperation.different:
                    return _v != _pack.toThat.Value;
                default:
                    return false;
            }
        }
    }
}
