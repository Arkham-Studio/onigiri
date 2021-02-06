using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [Serializable]
    [InlineProperty(LabelWidth = 12)]
    public class Vector2Reference
    {
        [HorizontalGroup(25), HideLabel()]
        public bool UseConstant = true;

        [HorizontalGroup(), HideLabel()]
        [ShowIf("UseConstant")]
        public Vector2 ConstantValue;

        [HorizontalGroup(), HideLabel()]
        [HideIf("UseConstant")]
        public Vector2Variable Variable;

        public Vector2Reference()
        { }

        public Vector2Reference(Vector2 value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public Vector2 Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator Vector2(Vector2Reference reference)
        {
            return reference.Value;
        }

        private static IEnumerable UseConstantValues = new ValueDropdownList<bool>() {
            {"Constant", true },
            {"Variable", false },
        };
    }
}
