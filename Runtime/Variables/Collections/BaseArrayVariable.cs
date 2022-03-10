using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;

namespace Arkham.Onigiri.Variables
{
    public class BaseArrayVariable<T> : BaseVariable<T[]>
    {

        [ReadOnly, SerializeField]
        private int selectedIndex = 0;

        public int Length => Value.Length;
        public T SelectedValue => Value[selectedIndex];
        public T this[int i]
        {
            get { return Value[i]; }
            set
            {
                if (value.Equals(Value[i]))
                    return;
                Value[i] = value;
                OnChange();
            }
        }

        private void OnEnable()
        {
            if (DefaultValue == null)
                DefaultValue = new T[0];

            Value = (T[])DefaultValue.Clone();
        }


        [Button]
        public void SetItem(int _index, T _value)
        {
            if (_index >= Length)
                return;
            if (_value.Equals(Value[_index]))
                return;
            Value[_index] = _value;
            OnChange();
        }

        public void SetItemQuiet(int _index, T _value)
        {
            if (_index >= Length)
                return;
            Value[_index] = _value;
        }

        public void SetItemAtSelected(T _value)
        {
            if (_value.Equals(Value[selectedIndex]))
                return;
            Value[selectedIndex] = _value;
            OnChange();
        }
        public void SetItemAtSelected(ChangeableVariable _variable)
        {
            if (_variable.GetType().IsSubclassOf(typeof(BaseVariable<T>)))
                SetItemAtSelected(((BaseVariable<T>)_variable).Value);
        }

        public void SetItemAtSelectedQuiet(T _value)
        {
            if (_value.Equals(Value[selectedIndex]))
                return;
            Value[selectedIndex] = _value;
        }
        public void SetItemAtSelectedQuiet(ChangeableVariable _variable)
        {
            if (_variable.GetType().IsSubclassOf(typeof(BaseVariable<T>)))
                SetItemAtSelectedQuiet(((BaseVariable<T>)_variable).Value);
        }

        [Button]
        public T SetSelected(int _index)
        {
            if (_index >= Length)
                return SelectedValue;
            if (_index == selectedIndex)
                return SelectedValue;
            selectedIndex = _index;
            OnChange();
            return SelectedValue;
        }

        public T SetSelectedQuiet(int _index)
        {
            if (_index >= Length)
                return SelectedValue;
            selectedIndex = _index;
            return SelectedValue;
        }

        public void IncrementSelected(int _value = 1)
        {
            selectedIndex = Mathf.Clamp(selectedIndex + _value, 0, Length - 1);
            OnChange();
        }

        public void IncrementSelectedQuiet(int _value = 1)
        {
            selectedIndex = Mathf.Clamp(selectedIndex + _value, 0, Length - 1);
        }

        public void IncrementSelectedLoop(int _value = 1)
        {
            selectedIndex = (selectedIndex + _value) % Length - 1;
            OnChange();
        }

        public void IncrementSelectedQuietLoop(int _value = 1)
        {
            selectedIndex = (selectedIndex + _value) % Length - 1;
        }

    }
}
