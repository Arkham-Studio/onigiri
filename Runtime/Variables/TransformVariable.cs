using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Transform")]
    public class TransformVariable : BaseVariable<Transform>
    {
        public override string ValueToString() => Value.gameObject.name;
        public override int ValueToInt() => Value.GetInstanceID();
        public override float ValueToFloat() => Value.GetInstanceID();
        public override bool ValueToBool() => Value.gameObject.activeSelf;
    }
}