﻿using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Array/Vector2")]
    public class Vector2ArrayVariable : BaseArrayVariable<Vector2>
    {
        public override int ValueToInt() => Mathf.RoundToInt(SelectedValue.magnitude);
        public override float ValueToFloat() => SelectedValue.magnitude;
        public override bool ValueToBool() => SelectedValue.magnitude > 0;
        public override string ValueToString(string format = "") => SelectedValue.ToString();
    }
}
