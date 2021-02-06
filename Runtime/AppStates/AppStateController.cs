using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class AppStateController : MonoBehaviour
{
    [SerializeField]
    private AppStateManager stateManager;

    [SerializeField]
    private DenumVariable state;

    [SerializeField]
    private DenumVariable nextState;

#if UNITY_EDITOR
    [TextArea(2, 10), HideLabel(), FoldoutGroup("Infos")]
    public string infos = "";
#endif
    [SerializeField]
    [Tooltip("Trigger for all app state controller at start, active or not")]
    private UnityEvent onInitState;

#if UNITY_EDITOR
    [TextArea(2, 10), HideLabel(), FoldoutGroup("Infos2")]
    public string infos2 = "";
#endif
    [SerializeField]
    private UnityEvent onEnterState;

#if UNITY_EDITOR
    [TextArea(2, 10), HideLabel(), FoldoutGroup("Infos3")]
    public string infos3 = "";
#endif
    [SerializeField]
    private UnityEvent onLeaveState;

    public bool forceInit = false;


    private void Start()
    {
        if (stateManager.actualState != state && !forceInit) onInitState?.Invoke();
        else onEnterState?.Invoke();

        stateManager.onStateChange.RegisterDelegate(OnStateChange);

    }

    public void OnStateChange()
    {
        if (stateManager.actualState == state) onEnterState?.Invoke();
        else if (stateManager.lastState == state) onLeaveState?.Invoke();
    }

    public void ChangeAppState(DenumVariable n)
    {
        stateManager.ChangeAppState(n);
    }

    public void GoNextState()
    {
        stateManager.ChangeAppState(nextState);
    }

#if UNITY_EDITOR
    [Button]
    public void SaveInfos()
    {
        string _infos = "";
        for (int i = 0; i < onEnterState.GetPersistentEventCount(); i++)
        {
            _infos += onEnterState.GetPersistentTarget(i).name + " => " + onEnterState.GetPersistentMethodName(i);
            _infos += "\n";
        }
        infos2 = _infos;

        _infos = "";
        for (int i = 0; i < onInitState.GetPersistentEventCount(); i++)
        {
            _infos += onInitState.GetPersistentTarget(i).name + " => " + onInitState.GetPersistentMethodName(i);
            _infos += "\n";
        }
        infos = _infos;

        _infos = "";
        for (int i = 0; i < onLeaveState.GetPersistentEventCount(); i++)
        {
            _infos += onLeaveState.GetPersistentTarget(i).name + " => " + onLeaveState.GetPersistentMethodName(i);
            _infos += "\n";
        }
        infos3 = _infos;
    }
#endif

}
