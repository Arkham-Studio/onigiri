using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.Variables
{
    public interface ITrackGameEvent
    {
        UnityEvent GetEvent();
    }

    [InlineEditor(InlineEditorObjectFieldModes.Foldout, DrawHeader = false)]
    public class ChangeableVariable : ScriptableObject
    {
        [PropertyOrder(1)]
        [FoldoutGroup("OnChange", false)]
        public UnityEvent onChange;
        public UnityEvent GetEvent() => onChange;
        [Button("Invoke OnChange", ButtonSizes.Large)]
        [PropertyOrder(2)]
        public void OnChange() => onChange?.Invoke();

    }

    public class BaseVariable<T> : ChangeableVariable, ITrackGameEvent
    {
        [SerializeField]
        protected T DefaultValue;

        public T currentValue;
        public T Value
        {
            get { return currentValue; }
            set { currentValue = value; }
        }

        private void OnEnable() => currentValue = DefaultValue;

        public void SetValue(T value)
        {
            Value = value;
            OnChange();
        }

        public void SetValue(BaseVariable<T> value)
        {
            Value = value.Value;
            OnChange();
        }

        public void SetToNull()
        {
            Value = default;
            OnChange();
        }

        public void Reset()
        {
            currentValue = DefaultValue;
            OnChange();
        }

        public void SetValueQuiet(T value) => Value = value;
        public void SetValueQuiet(BaseVariable<T> value) => Value = value.Value;
        public void ResetQuiet() => currentValue = DefaultValue;

        public static implicit operator T(BaseVariable<T> reference) => reference.Value;

        public T GetDefaultValue() => DefaultValue;
    }
}