using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
#pragma warning disable CS0649
namespace Arkham.Onigiri.Events
{
    public class ChangeableVariableListener : MonoBehaviour
    {
        [SerializeField] private ChangeableVariablePack[] pack;

        //  MONOS
        private void OnEnable()
        {
            foreach (var item in pack)
                item.variable.onChange.AddListener(item.OnChange);
        }

        private void OnDisable()
        {
            foreach (var item in pack)
                item.variable.onChange.RemoveListener(item.OnChange);
        }

        //  UTILS
        [System.Serializable]
        public class ChangeableVariablePack
        {
            [Title("@variable?.name")]
            public ChangeableVariable variable;
            [ShowIf("variable")]
            public UnityEvent response;

            [Button("Invode OnChange",ButtonSizes.Large),ShowIf("variable")]
            public void OnChange() => response.Invoke();
        }
    }
}
