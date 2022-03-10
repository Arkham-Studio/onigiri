using Arkham.Onigiri.Utils;
using Arkham.Onigiri.Variables;
using UnityEngine;

namespace Arkham.Onigiri.LogicModule
{
    public class IfColorVariable : BaseIfChangeableVariable<Color, ColorVariable, ColorReference>
    {

        public override bool Test(Color _v, ComparePack _pack)
        {
            switch (_pack.how)
            {
                case CompareOperation.equals:
                    return _v == _pack.toThat.Value;
                case CompareOperation.less:
                    return _v.Luminance() < _pack.toThat.Value.Luminance();
                case CompareOperation.more:
                    return _v.Luminance() > _pack.toThat.Value.Luminance();
                case CompareOperation.lessEqual:
                    return _v.Luminance() <= _pack.toThat.Value.Luminance();
                case CompareOperation.moreEqual:
                    return _v.Luminance() >= _pack.toThat.Value.Luminance();
                case CompareOperation.different:
                    return _v.Luminance() != _pack.toThat.Value.Luminance();
                default:
                    return false;
            }
        }
    }
}
