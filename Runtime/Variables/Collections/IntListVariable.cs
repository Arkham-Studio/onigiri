using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/List/Int")]
    public class IntListVariable : BaseListVariable<int>
    {
        public override string ValueToString() => SelectedValue.ToString();
        public override int ValueToInt() => SelectedValue;
        public override float ValueToFloat() => SelectedValue;
        public override bool ValueToBool() => SelectedValue > 0;
    }
}
