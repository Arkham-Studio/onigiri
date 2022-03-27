using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkham.Onigiri.Utils
{
    [DefaultExecutionOrder(-100)]
    public class ResetScriptableObject : MonoBehaviour
    {
        public ChangeableVariable[] intList;
        public FloatVariable[] floatList;
        public BoolVariable[] boolList;
        public StringVariable[] stringList;

        private void Awake() => SceneManager.sceneLoaded += LevelWasLoaded;

        private void LevelWasLoaded(Scene s, LoadSceneMode lsm)
        {
            foreach (FloatVariable i in floatList) i.ResetValue();
            foreach (IntVariable i in intList) i.ResetValue();
            foreach (BoolVariable i in boolList) i.ResetValue();
            foreach (StringVariable i in stringList) i.ResetValue();
        }
    }
}