using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/List/Vector2")]
    public class Vector2ListVariable : BaseListVariable<Vector2>
    {

        public override int ValueToInt() => Mathf.RoundToInt(SelectedValue.magnitude);
        public override float ValueToFloat() => SelectedValue.magnitude;
        public override bool ValueToBool() => SelectedValue.magnitude > 0;
        public override string ValueToString() => SelectedValue.ToString();
    }
}