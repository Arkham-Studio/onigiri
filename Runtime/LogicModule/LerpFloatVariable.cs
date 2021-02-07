#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.LogicModule
{
    [CreateAssetMenu(menuName = "Logic/Float Lerp")]
    public class LerpFloatVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea(2, 4), HideLabel()]
        public string infos;
#endif

        [SerializeField] private FloatReference a;
        [SerializeField] private FloatReference b;
        [SerializeField] private FloatReference c;

        [SerializeField] private FloatVariable output;
        public UnityEvent response;

        [Button]
        public void DoLerp()
        {
            output.SetValue(Mathf.Lerp(a.Value, b.Value, c.Value));
            response.Invoke();
        }

        [Button]
        public void DoInverseLerp()
        {
            output.SetValue(Mathf.InverseLerp(a.Value, b.Value, c.Value));
            response.Invoke();
        }

    }
}
