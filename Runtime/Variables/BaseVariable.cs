using Arkham.Onigiri.Attributes;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.Variables
{
    //public interface ITrackGameEvent
    //{
    //    UnityEvent GetEvent();
    //}

    public interface IVariableValueTo
    {
        string ValueToString();
        int ValueToInt();
        float ValueToFloat();
        bool ValueToBool();
    }


    [InlineEditor(InlineEditorObjectFieldModes.Foldout, DrawHeader = false,DisableGUIForVCSLockedAssets =true)]

    [System.Serializable]
    public class ChangeableVariable : ScriptableObject
    {
        [PropertyOrder(2)]
        [FoldoutGroup("OnChange", false)]
        public UnityEvent onChange;
        [Button("Invoke OnChange", ButtonSizes.Large), HorizontalGroup("Buttons")]
        [PropertyOrder(1)]
        public void OnChange() => onChange?.Invoke();
        //public UnityEvent GetEvent() => onChange;

    }

    [System.Serializable]
    [EditorIcon("onigiri-icon-v")]
    [InlineButton("@OnigiriEditorUtils.CreateSripteable($property)", "+", ShowIf = "@$value == null")]
    public class BaseVariable<T> : ChangeableVariable, IVariableValueTo
    //, ITrackGameEvent
    {
        [SerializeField]
        protected T DefaultValue;

        protected T currentValue;
        public T Value
        {
            get { return currentValue; }
        }
        public Type typeParameterType => typeof(T);


        private void OnEnable() => currentValue = DefaultValue;


        public void SetValue(T value)
        {
            currentValue = value;
            OnChange();
        }

        public void SetValue(BaseVariable<T> value) => SetValue(value.Value);

        public void SetToNull()
        {
            currentValue = default;
            OnChange();
        }

        [Button("Reset Value",ButtonSizes.Large), HorizontalGroup("Buttons")]
        public void ResetValue()
        {
            currentValue = DefaultValue;
            OnChange();
        }

        public void SetValueQuiet(T value) => currentValue = value;
        public void SetValueQuiet(BaseVariable<T> value) => currentValue = value.Value;
        public void ResetQuiet() => currentValue = DefaultValue;

        public static implicit operator T(BaseVariable<T> reference) => reference.Value;

        public T GetDefaultValue() => DefaultValue;

        public virtual string ValueToString() => name;

        public virtual int ValueToInt() => 0;

        public virtual float ValueToFloat() => 0;

        public virtual bool ValueToBool() => false;
    }
}