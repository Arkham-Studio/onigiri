using System.Collections;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/List/Float")]
    public class FloatListVariable : BaseListVariable<float>
    {
        public override string ValueToString() => SelectedValue.ToString();
        public override int ValueToInt() => Mathf.RoundToInt(SelectedValue);
        public override float ValueToFloat() => SelectedValue;
        public override bool ValueToBool() => SelectedValue > 0f;
    }
}