using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Arkham.Onigiri.UI
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleExtend : MonoBehaviour
    {
        [SerializeField] private Toggle myToggle;

        public UnityEvent onTrue;
        public UnityEvent onFalse;
        public UnityEvent<bool> onDynamic;

        private void Start()
        {
            if (myToggle == null) myToggle = GetComponent<Toggle>();
        }

        private void OnEnable() => myToggle.onValueChanged.AddListener(OnChange);
        private void OnDisable() => myToggle.onValueChanged.RemoveListener(OnChange);

        private void OnChange(bool _v)
        {
            if (_v) onTrue.Invoke();
            else onFalse.Invoke();

            onDynamic.Invoke(_v);
        }


    }
}
