using Arkham.Onigiri.Variables;
using Arkham.Onigiri.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.LogicModule
{
    public class IfVector2Variable : BaseIfChangeableVariable<Vector2, Vector2Variable, Vector2Reference>
    {
        public override bool Test(Vector2 _v, ComparePack _pack)
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
