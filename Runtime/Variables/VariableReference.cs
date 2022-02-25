//using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    //[InlineProperty]
    [Serializable]
    //[LabelWidth(75)]
    public class VariableReference <T>
    {
        //[HideLabel]
        //[LabelWidth(10)]
        //[HorizontalGroup("ref")]
        //[ValueDropdown("valueList", AppendNextDrawer = true, DisableGUIInAppendedDrawer = true)]
        public bool UseConstant = true;

        //[HideLabel]
        //[LabelWidth(100)]
        //[ShowIf("UseConstant", Animate =false)]
        //[HorizontalGroup("ref")]
        public T ConstantValue;

        //[HideLabel]
        //[LabelWidth(100)]
        //[HideIf("UseConstant", Animate = false)]
        //[HorizontalGroup("ref")]
        public BaseVariable<T> Variable;

        public VariableReference()
        { }

        public VariableReference(T value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public T Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator T(VariableReference<T> reference)
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