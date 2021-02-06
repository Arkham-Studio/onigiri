using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.Events
{
    [DefaultExecutionOrder(-1000)]
    public class UnityMonoEvents : MonoBehaviour
    {
        [Title("EVENTS")]
        [PropertySpace(0, 24)]
        [EnumToggleButtons, HideLabel, SerializeField]
        private MonosEvents events;
        [System.Flags]
        public enum MonosEvents { All = Awake | Start | Update | FixedUpdate | LateUpdate, Awake = 1 << 1, Start = 1 << 2, Update = 1 << 3, FixedUpdate = 1 << 4, LateUpdate = 1 << 5 }

        [ShowIf("@((int)events & (int)MonosEvents.Awake) != 0")]
        public UnityEvent onAwake;
        [ShowIf("@((int)events & (int)MonosEvents.Start) != 0")]
        public UnityEvent onStart;
        [ShowIf("@((int)events & (int)MonosEvents.Update) != 0")]
        public UnityEvent onUpdate;
        [ShowIf("@((int)events & (int)MonosEvents.FixedUpdate) != 0")]
        public UnityEvent onFixedUpdate;
        [ShowIf("@((int)events & (int)MonosEvents.LateUpdate) != 0")]
        public UnityEvent onLateUpdate;

        //  MONOS
        private void Awake() => onAwake.Invoke();

        private void Start() => onStart.Invoke();

        private void FixedUpdate() => onFixedUpdate.Invoke();

        private void Update() => onUpdate.Invoke();

        private void LateUpdate() => onLateUpdate.Invoke();
    }
}
