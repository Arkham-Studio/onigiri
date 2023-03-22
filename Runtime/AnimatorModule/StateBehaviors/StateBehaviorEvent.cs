using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Arkham.Onigiri.AnimatorModule
{
    public class StateBehaviorEvent : StateMachineBehaviour
    {
        [SerializeField, EnumToggleButtons, HideLabel] private EventsType showEvents = 0;

        [ShowIf("IsEnterEvent")]
        public UnityEvent onStateEnter;
        [ShowIf("IsUpdateEvent")]
        public UnityEvent onStateUpdate;
        [ShowIf("IsLoopEvent")]
        public UnityEvent onStateAnimLoop;
        [ShowIf("IsExitEvent")]
        public UnityEvent onStateExit;

        private bool haveLooped = false;
        private bool firstLoop = true;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (IsEnterEvent)
                onStateEnter.Invoke();

            haveLooped = false;
            firstLoop = true;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (IsUpdateEvent)
                onStateUpdate.Invoke();

            if (IsLoopEvent)
            {
                float _time = stateInfo.normalizedTime % 1;
                if (!haveLooped && _time < 0.3f && !firstLoop)
                {
                    haveLooped = true;
                    onStateAnimLoop.Invoke();
                }
                else if ((haveLooped || firstLoop) && _time > 0.3f)
                {
                    haveLooped = false;
                    firstLoop = false;
                }
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (IsExitEvent)
                onStateExit.Invoke();
        }

        //  ODIN
        bool IsEnterEvent => ((int)showEvents & (int)EventsType.Enter) != 0;
        bool IsUpdateEvent => ((int)showEvents & (int)EventsType.Update) != 0;
        bool IsLoopEvent => ((int)showEvents & (int)EventsType.Loop) != 0;
        bool IsExitEvent => ((int)showEvents & (int)EventsType.Exit) != 0;
        [System.Flags]
        public enum EventsType { Enter = 1 << 1, Update = 1 << 2, Loop = 1 << 3, Exit = 1 << 4, All = Enter | Update | Loop | Exit }
    }
}
