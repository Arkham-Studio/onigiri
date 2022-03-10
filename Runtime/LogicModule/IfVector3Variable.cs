using Arkham.Onigiri.Variables;
using UnityEngine;

namespace Arkham.Onigiri.LogicModule
{
    public class IfVector3Variable : BaseIfChangeableVariable<Vector3, Vector3Variable, Vector3Reference>
    {
        public override bool Test(Vector3 _v, ComparePack _pack)
        {
            switch (_pack.how)
            {
                case CompareOperation.equals:
                    return _v == _pack.toThat.Value;
                case CompareOperation.less:
                    return _v.sqrMagnitude < _pack.toThat.Value.sqrMagnitude;
                case CompareOperation.more:
                    return _v.sqrMagnitude > _pack.toThat.Value.sqrMagnitude;
                case CompareOperation.lessEqual:
                    return _v.sqrMagnitude <= _pack.toThat.Value.sqrMagnitude;
                case CompareOperation.moreEqual:
                    return _v.sqrMagnitude >= _pack.toThat.Value.sqrMagnitude;
                case CompareOperation.different:
                    return _v != _pack.toThat.Value;
                default:
                    return false;
            }
        }
    }
}