using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector3")]
    public class Vector4Variable : BaseVariable<Vector4>
    {
        public override int ValueToInt() => Mathf.RoundToInt(Value.magnitude);
        public override float ValueToFloat() => Value.magnitude;
        public override bool ValueToBool() => Value.magnitude > 0;
        public override string ValueToString(string format = "") => Value.ToString();

        public float magnitude { get => Value.magnitude; }
        public float sqrMagnitude { get => Value.sqrMagnitude; }
        public Vector4 normalized { get => Value.normalized; }
    }
}
