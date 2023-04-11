using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Arkham.Onigiri.Variables
{
    public class BaseHashsetVariable<T> : BaseVariable<HashSet<T>>
    {

        public int Count => Value.Count;

        private T lastAdded;
        public T LastAdded
        {
            get { return lastAdded; }
        }


        private void OnEnable()
        {
            currentValue = new HashSet<T>();
        }

        public void AddItem(ChangeableVariable _variable)
        {
            if (_variable.GetType().IsSubclassOf(typeof(BaseVariable<T>)))
                AddItem(((BaseVariable<T>)_variable).Value);
        }
        [Button]
        public void AddItem(T _v)
        {
            if (Value.Contains(_v))
                return;
            lastAdded = _v;
            Value.Add(_v);
            OnChange();
        }
        public void AddItemQuiet(ChangeableVariable _variable)
        {
            if (_variable.GetType().IsSubclassOf(typeof(BaseVariable<T>)))
                AddItemQuiet(((BaseVariable<T>)_variable).Value);
        }
        public void AddItemQuiet(T _v)
        {
            if (Value.Contains(_v))
                return;
            lastAdded = _v;
            Value.Add(_v);
        }
        public bool RemoveItem(T _v)
        {
            bool _result = Value.Remove(_v);
            if (_result)
                OnChange();
            return _result;
        }
        public bool RemoveItemQuiet(T _v) => Value.Remove(_v);

        public void Clear()
        {
            Value.Clear();
            OnChange();
        }

        public void ClearQuiet() => Value.Clear();
    }
}
