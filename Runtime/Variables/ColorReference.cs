//using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    //[InlineProperty]
    [Serializable]
    //[LabelWidth(75)]
    public class ColorReference
    {

        //public ColorReference(Color _c)
        //{
        //    ConstantValue = _c;
        //    UseConstant = true;
        //}

        //[HideLabel]
        //[LabelWidth(10)]
        //[HorizontalGroup("ref")]
        //[ValueDropdown("valueList", AppendNextDrawer = true, DisableGUIInAppendedDrawer = true)]
        public bool UseConstant = true;

        //[HideLabel]
        //[LabelWidth(100)]
        //[ShowIf("UseConstant", Animate =false)]
        //[HorizontalGroup("ref")]
        public Color ConstantValue;

        //[HideLabel]
        //[LabelWidth(100)]
        //[HideIf("UseConstant", Animate = false)]
        //[HorizontalGroup("ref")]
        public ColorVariable Variable;

        public ColorReference()
        { }

        public ColorReference(Color value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public Color Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator Color(ColorReference reference)
        {
            return reference.Value;
        }


        //private static ValueDropdownList<bool> valueList = new ValueDropdownList<bool>()
        //{
        //    {"constant", true },
        //    {"variable", false }
        //};
    }
}