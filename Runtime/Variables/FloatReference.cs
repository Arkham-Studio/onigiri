//using Sirenix.OdinInspector;
using System;

namespace Arkham.Onigiri.Variables
{
    //[InlineProperty]
    [Serializable]
    //[LabelWidth(75)]
    public class FloatReference
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
        public float ConstantValue;

        //[HideLabel]
        //[LabelWidth(100)]
        //[HideIf("UseConstant", Animate = false)]
        //[HorizontalGroup("ref")]
        public FloatVariable Variable;

        public FloatReference()
        { }

        public FloatReference(float value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public float Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator float(FloatReference reference)
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