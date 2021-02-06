using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;

public class AppStateController2 : MonoBehaviour
{

    [SerializeField]
    private DenumVariable actualState;
    [SerializeField]
    private Denum[] state;
    [SerializeField]
    private Denum nextState;
    [SerializeField]
    private bool forceInit = false;

    [SerializeField]
    [Tooltip("Trigger for all app state controller at start, active or not")]
    private UnityEvent onInitState;

    [SerializeField]
    private UnityEvent onEnterState;

    [SerializeField]
    private UnityEvent onLeaveState;

    private bool isActive = false;

    private void Start()
    {
        actualState.onChange.AddListener(OnStateChange);

        if (!IsState(actualState.Value) && !forceInit)
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
        if (IsState(actualState.Value))
        {
            isActive = true;
            onEnterState?.Invoke();
        }
        else if (isActive)
        {
            isActive = false;
            onLeaveState?.Invoke();
        }
    }

    public void ChangeAppState(Denum n) => actualState.SetValue(n);

    public void GoNextState() => actualState.SetValue(nextState);

    private bool IsState(Denum _state)
    {
        foreach (Denum item in state)
            if (item == _state) return true;
        return false;
    }
}
