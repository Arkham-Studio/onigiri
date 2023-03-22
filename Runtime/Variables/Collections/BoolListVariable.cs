using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/List/Bool")]
    public class BoolListVariable : BaseListVariable<bool>
    {
        public override string ValueToString(string format = "") => SelectedValue.ToString();
        public override int ValueToInt() => SelectedValue ? 1 : 0;
        public override float ValueToFloat() => SelectedValue ? 1f : 0f;
        public override bool ValueToBool() => SelectedValue;
    }
}