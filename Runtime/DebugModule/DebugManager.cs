using Arkham.Onigiri.Variables;
using UnityEngine;

namespace Arkham.Onigiri.DebugModule
{
    [CreateAssetMenu(menuName = "Managers/DebugManager")]
    public class DebugManager : ScriptableObject
    {
        public void Log(string _value) => Debug.Log(_value);
        public void Log(StringVariable _value) => Debug.Log(_value.Value);
        public void Log(IntVariable _value) => Debug.Log(_value.Value);
        public void Log(FloatVariable _value) => Debug.Log(_value.Value);
        public void Log(Component _a) => Debug.Log(_a);
        public void Log(Component _a, Component _b) => Debug.Log(_a + " & " + _b);
    }
}
