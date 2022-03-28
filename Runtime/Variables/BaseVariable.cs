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

    public interface IVariableValueFrom
    {
        void StringToValue(string _s);
        void IntToValue(int _i);
        void FloatToValue(float _f);
        void BoolToValue(bool _b);
        void Vector2ToValue(Vector2 _v);
    }

    public interface IVariableResetable
    {
        void ResetValue();
    }


    [InlineEditor(InlineEditorObjectFieldModes.Foldout, DrawHeader = false,DisableGUIForVCSLockedAssets =true)]

    [System.Serializable]
    public class ChangeableVariable : ScriptableObject
    {
        [FoldoutGroup("OnChange", false), PropertyOrder(2)]
        public UnityEvent onChange;
        [Button("Invoke OnChange", ButtonSizes.Large), HorizontalGroup("Buttons"), PropertyOrder(1)]
        public void OnChange() => onChange?.Invoke();
        public UnityEvent GetEvent() => onChange;
    }

    [System.Serializable]
    [EditorIcon("onigiri-icon-v")]
    [InlineButton("@OnigiriEditorUtils.CreateSripteable($property)", "+", ShowIf = "@$value == null")]
    public class BaseVariable<T> : ChangeableVariable, IVariableValueTo, IVariableValueFrom, IVariableResetable
    //, ITrackGameEvent
    {
        [SerializeField]
        protected T DefaultValue;
        [SerializeField, ReadOnly, ShowIf("@UnityEngine.Application.isPlaying == true")]
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

        public virtual void StringToValue(string _v) => throw new NotImplementedException("Variable does not support this type : " + _v.GetType());
        public virtual void IntToValue(int _v) => throw new NotImplementedException("Variable does not support this type : " + _v.GetType());
        public virtual void FloatToValue(float _v) => throw new NotImplementedException("Variable does not support this type : " + _v.GetType());
        public virtual void BoolToValue(bool _v) => throw new NotImplementedException("Variable does not support this type : " + _v.GetType());
        public virtual void Vector2ToValue(Vector2 _v) => throw new NotSupportedException("Variable does not support this type : "+ _v.GetType());
        
    }
}