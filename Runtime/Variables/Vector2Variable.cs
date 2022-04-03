using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector2")]
    public class Vector2Variable : BaseVariable<Vector2>
    {
        public override int ValueToInt() => Mathf.RoundToInt(Value.magnitude);
        public override float ValueToFloat() => Value.magnitude;
        public override bool ValueToBool() => Value.magnitude > 0;
        public override string ValueToString() => Value.ToString();


        public override void Vector2ToValue(Vector2 _v) => SetValue(_v);
        public override void FloatToValue(float _v) => SetValue(new Vector2(_v, _v));


        public void ApplyChange(Vector2 _v)
        {
            SetValue(Value + _v);
        }
    }
}
