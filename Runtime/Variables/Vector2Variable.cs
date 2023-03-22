using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector2")]
    public class Vector2Variable : BaseVariable<Vector2>
    {
        public void ApplyChange(Vector2 _v) => SetValue(Value + _v);
        public void ApplyChange(Vector3 _v) => SetValue(Value + (Vector2)_v);
        public void ApplyChange(Vector4 _v) => SetValue(Value + (Vector2)_v);


        public override int ValueToInt() => Mathf.RoundToInt(Value.magnitude);
        public override float ValueToFloat() => Value.magnitude;
        public override bool ValueToBool() => Value.magnitude > 0;
        public override string ValueToString(string format = "0") => Value.ToString();


        public override void Vector2ToValue(Vector2 _v) => SetValue(_v);
        public override void FloatToValue(float _v) => SetValue(new Vector2(_v, _v));

        public static implicit operator float(Vector2Variable _v) => _v.Value.magnitude;
        public static Vector2Variable operator +(Vector2Variable _i, Vector2 _v)
        {
            _i.ApplyChange(_v);
            return _i;
        }
        public static Vector2Variable operator +(Vector2Variable _i, Vector3 _v)
        {
            _i.ApplyChange(_v);
            return _i;
        }
        public static Vector2Variable operator +(Vector2Variable _i, Vector4 _v)
        {
            _i.ApplyChange(_v);
            return _i;
        }
        public static Vector2Variable operator +(Vector2Variable _i, float _v)
        {
            _i.ApplyChange(Vector2.one * _v);
            return _i;
        }

        public float magnitude { get => Value.magnitude; }
        public float sqrMagnitude { get => Value.sqrMagnitude; }
        public Vector2 normalized { get => Value.normalized; }

    }
}
