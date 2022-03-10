//using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    //[InlineProperty]
    [Serializable]
    //[LabelWidth(75)]
    public class BaseVariableReference<T, U> where U : BaseVariable<T>
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
        public U Variable;

        public BaseVariableReference()
        { }

        public BaseVariableReference(T value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public T Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator T(BaseVariableReference<T, U> reference) => reference.Value;

        //private static ValueDropdownList<bool> valueList = new ValueDropdownList<bool>()
        //{
        //    {"constant", true },
        //    {"variable", false }
        //};
    }

    public interface IReferenceDrawer { }
}