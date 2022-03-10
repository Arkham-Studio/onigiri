using Arkham.Onigiri.Variables;

namespace Arkham.Onigiri.LogicModule
{
    public class IfFloatVariable : BaseIfChangeableVariable<float, FloatVariable, FloatReference>
    {

        public override bool Test(float _value, ComparePack _pack)
        {
            switch (_pack.how)
            {
                case CompareOperation.equals:
                    return _value == _pack.toThat.Value;
                case CompareOperation.less:
                    return _value > _pack.toThat.Value;
                case CompareOperation.more:
                    return _value < _pack.toThat.Value;
                case CompareOperation.lessEqual:
                    return _value >= _pack.toThat.Value;
                case CompareOperation.moreEqual:
                    return _value <= _pack.toThat.Value;
                case CompareOperation.different:
                    return _value != _pack.toThat.Value;
                default:
                    return false;
            }
        }

    }
}
