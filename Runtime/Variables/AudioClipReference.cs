using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [InlineProperty]
    [Serializable]
    [LabelWidth(75)]
    public class AudioClipReference
    {
        [EnumToggleButtons, HideLabel, OnValueChanged("SetConstantOnEnum")]
        public RefType refType;
        public enum RefType { constant, variable}

        //[HideLabel]
        //[LabelWidth(0)]
        //[HorizontalGroup("ref")]
        //[ValueDropdown("valueList", AppendNextDrawer = true, DisableGUIInAppendedDrawer = true)]
        [HideInInspector]
        public bool UseConstant = true;

        [HideLabel]
        //[LabelWidth(100)]
        //[HorizontalGroup("ref")]
        [ShowIf("UseConstant", Animate = false)]
        public AudioClip ConstantValue;

        [HideLabel]
        //[LabelWidth(100)]
        //[HorizontalGroup("ref")]
        [HideIf("UseConstant", Animate = false)]
        public AudioClipVariable Variable;

        public AudioClipReference()
        { }

        public AudioClipReference(AudioClip value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public AudioClip Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator AudioClip(AudioClipReference reference)
        {
            return reference.Value;
        }


        private static ValueDropdownList<bool> valueList = new ValueDropdownList<bool>()
        {
            {"constant", true },
            {"variable", false }
        };

        void SetConstantOnEnum()
        {
            UseConstant = refType == RefType.constant;
        }
    }
}