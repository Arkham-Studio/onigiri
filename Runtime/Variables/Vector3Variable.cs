using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector3")]
    public class Vector3Variable : BaseVariable<Vector3>
    {
        public void ApplyChange(Vector2 _v) => SetValue(Value + (Vector3)_v);
        public void ApplyChange(Vector3 _v) => SetValue(Value + _v);
        public void ApplyChange(Vector4 _v) => SetValue(Value + (Vector3)_v);

        public override int ValueToInt() => Mathf.RoundToInt(Value.magnitude);
        public override float ValueToFloat() => Value.magnitude;
        public override bool ValueToBool() => Value.magnitude > 0;
        public override string ValueToString() => Value.ToString();

        public float magnitude { get => Value.magnitude; }
        public float sqrMagnitude { get => Value.sqrMagnitude; }
        public Vector3 normalized { get => Value.normalized; }
    }
}

