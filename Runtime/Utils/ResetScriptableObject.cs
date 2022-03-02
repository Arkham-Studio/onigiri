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
            foreach (FloatVariable i in floatList) i.Reset();
            foreach (IntVariable i in intList) i.Reset();
            foreach (BoolVariable i in boolList) i.Reset();
            foreach (StringVariable i in stringList) i.Reset();
        }
    }
}