using UnityEngine;

[CreateAssetMenu(menuName = "Managers/App State Manager")]
public class AppStateManager : ScriptableObject
{
    [Header("States")]
    public GameEvent onStateChange;
    public DenumVariable startState;
    public DenumVariable actualState;
    public DenumVariable lastState;

    private void OnEnable()
    {
        actualState = startState;
    }

    public void ChangeAppState(DenumVariable n)
    {
        lastState = actualState;
        actualState = n;

        onStateChange.Raise();
    }

    public void QuitApp()
    {
        Application.Quit();
    }

}
