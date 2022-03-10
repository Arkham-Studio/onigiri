using Arkham.Onigiri.Variables;
using Arkham.Onigiri.Utils;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
#pragma warning disable CS0649
namespace Arkham.Onigiri.LogicModule
{
    public abstract class BaseIfChangeableVariable<T, U, V> : MonoBehaviour where U : BaseVariable<T>
    {
        [PropertyOrder(-2)] public U compareThis;

        [HideInInspector] public bool onStart = true;
        [HideInInspector] public bool listenChange = true;

        [ShowIf("@compareThis != null"), LabelText("To That"), PropertyOrder(2)] public ComparePack[] packs;

        private void OnEnable()
        {
            if (compareThis == null)
                return;
            if (listenChange)
                compareThis.onChange.AddListener(Compare);
            if (!onStart)
                return;
            Compare();
        }

        private void OnDisable()
        {
            if (compareThis == null)
                return;
            if (listenChange)
                compareThis.onChange.RemoveListener(Compare);
        }

        [Button("Compare", ButtonSizes.Medium), ShowIf("@compareThis != null"),PropertyOrder(0)]
        public void Compare()
        {
            if (compareThis == null)
                return;
            CompareWith(compareThis.Value);
        }

        public void CompareWith(T _v)
        {
            foreach (ComparePack item in packs)
            {
                bool _result = Test(_v, item) ^ item.isInversed;
                if (_result && (((int)item.eventType & (int)EventType.OnTrue) != 0))
                    item.onTrue.Invoke();
                else if (((int)item.eventType & (int)EventType.OnFalse) != 0)
                    item.onFalse.Invoke();
                if (((int)item.eventType & (int)EventType.Dynamic) != 0)
                    item.dynamicResponse.Invoke(_result);
            }
        }

        public virtual bool Test(T _value, ComparePack _pack) => false;
        


        [Button("On Start"), GUIColor("@onStart ? Color.white : Color.gray"), PropertyOrder(-1), ButtonGroup("Bool")]
        void ToggleOnStar() => onStart = !onStart;

        [Button("Listen Change"), GUIColor("@listenChange ? Color.white : Color.gray"), PropertyOrder(-1), ButtonGroup("Bool")]
        void ToggleListenChange() => listenChange = !listenChange;

        public enum CompareOperation { equals, less, more, lessEqual, moreEqual, different }
        [System.Flags]
        public enum EventType { OnTrue = 1 << 1, OnFalse = 1 << 2, Dynamic = 1 << 3, All = OnTrue | OnFalse | Dynamic }


        [System.Serializable]
        public class ComparePack
        {
            [HorizontalGroup("params"), HideLabel] public CompareOperation how = CompareOperation.equals;
            [HorizontalGroup("params"), HideLabel] public V toThat;

            [EnumToggleButtons, HideLabel]
            public EventType eventType;

            [ShowIf("@((int)eventType & (int)EventType.OnTrue) != 0")] public UnityEvent onTrue;
            [ShowIf("@((int)eventType & (int)EventType.OnFalse) != 0")] public UnityEvent onFalse;
            [ShowIf("@((int)eventType & (int)EventType.Dynamic) != 0")] public bool isInversed;
            [ShowIf("@((int)eventType & (int)EventType.Dynamic) != 0")] public BoolUnityEvent dynamicResponse;
        }
    }
}
