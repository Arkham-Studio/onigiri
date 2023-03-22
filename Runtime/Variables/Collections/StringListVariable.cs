using System.Collections;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/List/String")]
    public class StringListVariable : BaseListVariable<string>
    {
        public override string ValueToString(string format = "") => SelectedValue;
        public override int ValueToInt() => int.TryParse(SelectedValue, out int _result) ? _result : 0;
        public override float ValueToFloat() => float.TryParse(SelectedValue, out float _result) ? _result : 0f;
        public override bool ValueToBool() => SelectedValue.Length > 0;
    }
}