using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Mono used for call the Reset method on Variables at OnLevelWasLoaded
/// </summary>
[DefaultExecutionOrder(-100)]
public class ResetScriptableObject : MonoBehaviour
{
    public IntVariable[] intList;
    public FloatVariable[] floatList;
    public BoolVariable[] boolList;
    public StringVariable[] stringList;

    //[Header("TO INIT ENABLE EVENTS")]
    //public ScriptableObject[] toInitManagers;

    private void Awake() => SceneManager.sceneLoaded += LevelWasLoaded;

    private void LevelWasLoaded(Scene s, LoadSceneMode lsm)
    {
        foreach (FloatVariable i in floatList) i.Reset();
        foreach (IntVariable i in intList) i.Reset();
        foreach (BoolVariable i in boolList) i.Reset();
        foreach (StringVariable i in stringList) i.Reset();
    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    foreach (FloatVariable i in floatList) i.Reset();
    //    foreach (IntVariable i in intList) i.Reset();
    //    foreach (BoolVariable i in boolList) i.Reset();
    //    foreach (StringVariable i in stringList) i.Reset();
    //}
}