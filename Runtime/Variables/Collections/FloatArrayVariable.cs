using System.Collections;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Array/Float")]
    public class FloatArrayVariable : BaseArrayVariable<float>
    {
        public override string ValueToString(string format = "0.00") => SelectedValue.ToString(format);
        public override int ValueToInt() => Mathf.RoundToInt(SelectedValue);
        public override float ValueToFloat() => SelectedValue;
        public override bool ValueToBool() => SelectedValue > 0f;
    }
}