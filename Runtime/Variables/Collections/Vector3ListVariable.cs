using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/List/Vector3")]
    public class Vector3ListVariable : BaseListVariable<Vector3>
    {

        public override int ValueToInt() => Mathf.RoundToInt(SelectedValue.magnitude);
        public override float ValueToFloat() => SelectedValue.magnitude;
        public override bool ValueToBool() => SelectedValue.magnitude > 0;
        public override string ValueToString(string format = "") => SelectedValue.ToString();
    }
}