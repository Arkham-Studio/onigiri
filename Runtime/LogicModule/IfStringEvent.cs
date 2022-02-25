﻿using Arkham.Onigiri.Variables;
using Arkham.Onigiri.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.LogicModule
{
    public class IfStringEvent : MonoBehaviour
    {
        [PropertyOrder(-2)] public StringVariable toCompare;

        [HideInInspector] public bool onStart = true;
        [HideInInspector] public bool listenChange = true;

        public ComparePackFloat[] packs;

        private void OnEnable()
        {
            if (toCompare == null) return;
            if (listenChange) toCompare.onChange.AddListener(Compare);
            if (!onStart) return;
            Compare();
        }

        private void OnDisable()
        {
            if (toCompare == null) return;
            if (listenChange) toCompare.onChange.RemoveListener(Compare);
        }

        public void Compare()
        {
            if (toCompare == null) return;
            CompareWith(toCompare.Value);
        }

        public void CompareWith(string _v)
        {
            foreach (ComparePackFloat item in packs)
            {
                bool _result = item.Test(_v) ^ item.isInversed;
                if (_result && (((int)item.eventType & (int)EventType.OnTrue) != 0)) item.onTrue.Invoke();
                else if (((int)item.eventType & (int)EventType.OnFalse) != 0) item.onFalse.Invoke();
                if (((int)item.eventType & (int)EventType.Dynamic) != 0) item.dynamicResponse.Invoke(_result);
            }
        }

        [Button("On Start"), GUIColor("@onStart ? Color.white : Color.gray"), PropertyOrder(-1), ButtonGroup("Bool")]
        void ToggleOnStar() => onStart = !onStart;

        [Button("Listen Change"), GUIColor("@listenChange ? Color.white : Color.gray"), PropertyOrder(-1), ButtonGroup("Bool")]
        void ToggleListenChange() => listenChange = !listenChange;

        public enum CompareOperation { equals, less, more, lessEqual, moreEqual, different }
        [System.Flags]
        public enum EventType { OnTrue = 1 << 1, OnFalse = 1 << 2, Dynamic = 1 << 3, All = OnTrue | OnFalse | Dynamic }
        [System.Serializable]
        public class ComparePackFloat
        {
            [HorizontalGroup("params"), HideLabel] public CompareOperation how = CompareOperation.equals;
            [HorizontalGroup("params"), HideLabel] public string value;

            [EnumToggleButtons, HideLabel]
            public EventType eventType;

            [ShowIf("@((int)eventType & (int)EventType.OnTrue) != 0")] public UnityEvent onTrue;
            [ShowIf("@((int)eventType & (int)EventType.OnFalse) != 0")] public UnityEvent onFalse;
            [ShowIf("@((int)eventType & (int)EventType.Dynamic) != 0")] public bool isInversed;
            [ShowIf("@((int)eventType & (int)EventType.Dynamic) != 0")] public BoolUnityEvent dynamicResponse;

            public bool Test(string _v)
            {
                switch (how)
                {
                    case CompareOperation.equals:
                        return value.Equals(_v);
                    case CompareOperation.different:
                        return !value.Equals(_v);
                    case CompareOperation.less:
                        return value.Length < _v.Length;
                    case CompareOperation.more:
                        return value.Length > _v.Length;
                    case CompareOperation.lessEqual:
                        return value.Length <= _v.Length;
                    case CompareOperation.moreEqual:
                        return value.Length >= _v.Length;
                    default:
                        return false;
                }
            }
        }
    }
}
