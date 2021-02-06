using System.Collections;
using System.Collections.Generic;
using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

//TODO NOT WORKING
public class GlobalIfEvent<T> : ScriptableObject where T : BaseVariable<T> 
{

    [SerializeField] private IfEventPack[] packs;

    [System.Serializable]
    public class IfEventPack
    {
#if UNITY_EDITOR
        [TextArea(2, 4), HideLabel()]
        public string infos;
#endif
        [SerializeField] private T a;
        [SerializeField] private T b;
        [SerializeField] private TestType test;
        public UnityEvent response;

        public void Test()
        {
            switch (test)
            {
                case TestType.more:
                    break;
                case TestType.less:
                    break;
                case TestType.moreEqual:
                    break;
                case TestType.lessEqual:
                    break;
                case TestType.equal:
                    break;
                default:
                    break;
            }
        }
    }

    public enum TestType { more, less, moreEqual, lessEqual, equal }
}
