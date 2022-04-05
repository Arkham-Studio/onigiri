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


        public static implicit operator string(TransformVariable reference) => reference.Value.gameObject.name;
        public static implicit operator int(TransformVariable reference) => reference.Value.GetInstanceID();
        public static implicit operator float(TransformVariable reference) => reference.Value.GetInstanceID();
        public static implicit operator bool(TransformVariable reference) => reference.Value.gameObject.activeSelf;


    }
}