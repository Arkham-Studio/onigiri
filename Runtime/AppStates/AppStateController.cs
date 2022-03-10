#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using Arkham.Onigiri.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AppStates
{
    public class AppStateController : MonoBehaviour
    {

        [SerializeField]
        private DenumVariable actualState;
        [SerializeField]
        private Denum[] states;


        [SerializeField]
        [Tooltip("Force OnEnter when active and changing for state in list")]
        private bool reTrigger = true;

        [SerializeField]
        private UnityEvent onInitState;

        [SerializeField]
        private UnityEvent onEnterState;

        [SerializeField]
        private UnityEvent onLeaveState;

        private bool isActive = false;

        private void Start()
        {
            actualState.onChange.AddListener(OnStateChange);

            if (!HaveState(actualState.Value))
            {
                isActive = false;
                onInitState?.Invoke();
            }
            else
            {
                isActive = true;
                onEnterState?.Invoke();
            }
        }

        public void OnStateChange()
        {
            if (HaveState(actualState.Value) && isActive && reTrigger)
            {
                isActive = true;
                onEnterState?.Invoke();
            }
            else if (HaveState(actualState.Value) && !isActive)
            {
                isActive = true;
                onEnterState?.Invoke();
            }
            else if (!HaveState(actualState.Value) && isActive)
            {
                isActive = false;
                onLeaveState?.Invoke();
            }
        }

        public void ChangeAppState(Denum n) => actualState.SetValue(n);
        public void ChangeAppStateRandom() => actualState.SetValue(states.GetRandom());

        private bool HaveState(Denum _state)
        {
            foreach (Denum item in states)
                if (item == _state)
                    return true;
            return false;
        }
    }
}
