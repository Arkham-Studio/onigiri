using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector2")]
    public class Vector2Variable : BaseVariable<Vector2>
    {
        public override int ValueToInt() => Mathf.RoundToInt( Value.magnitude);
        public override float ValueToFloat() => Value.magnitude;
        public override bool ValueToBool() => Value.magnitude > 0;
        public override string ValueToString() => Value.ToString();
    }
}
