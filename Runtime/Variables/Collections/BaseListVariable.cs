using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    public class BaseListVariable<T> : BaseVariable<List<T>>
    {
        [ReadOnly, SerializeField]
        private int selectedIndex = 0;

        public int Count => Value.Count;
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
                DefaultValue = new List<T>();
            Value = new List<T>(DefaultValue);
        }

        public void AddItem(ChangeableVariable _variable)
        {
            if (_variable.GetType().IsSubclassOf(typeof(BaseVariable<T>)))
                AddItem(((BaseVariable<T>)_variable).Value);
        }
        [Button]
        public void AddItem(T _v)
        {
            Value.Add(_v);
            OnChange();
        }
        public void AddItemQuiet(ChangeableVariable _variable)
        {
            if (_variable.GetType().IsSubclassOf(typeof(BaseVariable<T>)))
                AddItemQuiet(((BaseVariable<T>)_variable).Value);
        }
        public void AddItemQuiet(T _v) => Value.Add(_v);
        public void RemoveItemAt(int _i)
        {
            if (_i >= Count)
                return;
            Value.RemoveAt(_i);
            OnChange();
        }
        public void RemoveItemAtQuiet(int _i) => Value.RemoveAt(_i);
        public bool RemoveItem(T _v)
        {
            bool _result = Value.Remove(_v);
            if (_result)
                OnChange();
            return _result;
        }
        public bool RemoveItemQuiet(T _v) => Value.Remove(_v);

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
            if (_index >= Count || _index == selectedIndex)
                return SelectedValue;
            selectedIndex = _index;
            OnChange();
            return SelectedValue;
        }

        public T SetSelectedQuiet(int _index)
        {
            if (_index >= Count || _index == selectedIndex)
                return SelectedValue;
            selectedIndex = _index;
            return SelectedValue;
        }

        public void IncrementSelected(int _value = 1)
        {
            selectedIndex = Mathf.Clamp(selectedIndex + _value, 0, Count - 1);
            OnChange();
        }

        public void IncrementSelectedQuiet(int _value = 1)
        {
            selectedIndex = Mathf.Clamp(selectedIndex + _value, 0, Count - 1);
        }

        public void IncrementSelectedLoop(int _value = 1)
        {
            selectedIndex = (selectedIndex + _value) % Count - 1;
            OnChange();
        }

        public void IncrementSelectedQuietLoop(int _value = 1)
        {
            selectedIndex = (selectedIndex + _value) % Count - 1;
        }

    }
}