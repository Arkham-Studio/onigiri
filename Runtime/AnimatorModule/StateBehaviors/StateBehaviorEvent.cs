using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Arkham.Onigiri.AnimatorModule
{
    public class StateBehaviorEvent : StateMachineBehaviour
    {
        [SerializeField, EnumToggleButtons, HideLabel] private EventsType showEvents = EventsType.All;

        [ShowIf("IsEnterEvent")]
        public UnityEvent onStateEnter;
        [ShowIf("IsUpdateEvent")]
        public UnityEvent onStateUpdate;
        [ShowIf("IsLoopEvent")]
        public UnityEvent onStateAnimLoop;
        [ShowIf("IsExitEvent")]
        public UnityEvent onStateExit;

        private bool haveLooped = false;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => onStateEnter.Invoke();

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            onStateUpdate.Invoke();

            float _time = stateInfo.normalizedTime % 1;

            if (haveLooped && _time < 0.99f) haveLooped = false;

            if (!haveLooped && _time > 0.98f)
            {
                haveLooped = true;
                onStateAnimLoop.Invoke();
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => onStateExit.Invoke();

        //  ODIN
        bool IsEnterEvent => ((int)showEvents & (int)EventsType.Enter) != 0;
        bool IsUpdateEvent => ((int)showEvents & (int)EventsType.Update) != 0;
        bool IsLoopEvent => ((int)showEvents & (int)EventsType.Loop) != 0;
        bool IsExitEvent => ((int)showEvents & (int)EventsType.Exit) != 0;
        [System.Flags]
        public enum EventsType { Enter = 1 << 1, Update = 1 << 2, Loop = 1 << 3, Exit = 1 << 4, All = Enter | Update | Loop | Exit }
    }
}
