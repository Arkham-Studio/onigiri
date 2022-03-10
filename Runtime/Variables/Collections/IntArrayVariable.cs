using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Array/Int")]
    public class IntArrayVariable : BaseArrayVariable<int>
    {
        public override string ValueToString() => SelectedValue.ToString();
        public override int ValueToInt() => SelectedValue;
        public override float ValueToFloat() => SelectedValue;
        public override bool ValueToBool() => SelectedValue > 0;
    }
}
