using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector2Int")]
    public class Vector2IntVariable : BaseVariable<Vector2Int>
    {
        public override int ValueToInt() => Mathf.RoundToInt(Value.magnitude);
        public override float ValueToFloat() => Value.magnitude;
        public override bool ValueToBool() => Value.magnitude > 0;
        public override string ValueToString() => Value.ToString();
    }
}
