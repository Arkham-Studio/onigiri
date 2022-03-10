using Arkham.Onigiri.Variables;
using Arkham.Onigiri.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.LogicModule
{
    public class IfDenumEvent : BaseIfChangeableVariable<Denum, DenumVariable, DenumReference>
    {
        public override bool Test(Denum _value, ComparePack _pack)
        {
            switch (_pack.how)
            {
                case CompareOperation.equals:
                    return _value.name == _pack.toThat.Value.name;
                case CompareOperation.different:
                    return _value.name != _pack.toThat.Value.name;
                default:
                    return _value.name == _pack.toThat.Value.name;
            }
        }
    }
}
