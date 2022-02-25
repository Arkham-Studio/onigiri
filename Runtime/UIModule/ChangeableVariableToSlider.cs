using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Arkham.Onigiri.UI
{
    public class ChangeableVariableToSlider : MonoBehaviour
    {
        [SerializeField] private Slider mySlider;
        [SerializeField] private ChangeableVariable variable;
        public ChangeableVariable Variable
        {
            set { variable = value; }
        }
        [SerializeField] private bool initOnStart = true;
        [SerializeField] private bool listenToChange = true;
        [SerializeField] private bool inverted = false;

        [SerializeField] private bool isAutoAnimated = false;
        [SerializeField] private float smooth = 1f;

        private float targetValue = 0;

        private void OnEnable()
        {
            if (listenToChange)
                variable.onChange.AddListener(UpdateValue);
        }

        private void OnDisable()
        {
            if (listenToChange)
                variable.onChange.RemoveListener(UpdateValue);
        }

        void Start()
        {
            mySlider = mySlider ?? GetComponent<Slider>();
            if (initOnStart) UpdateValue();
        }

        void FixedUpdate()
        {
            if (!isAutoAnimated) return;
            mySlider.value = Mathf.Lerp(mySlider.value, targetValue, Time.fixedDeltaTime/ smooth);
        }

        public void UpdateValue()
        {
            switch (variable)
            {
                case FloatVariable f:
                    targetValue = f.Value;
                    break;
                case IntVariable i:
                    targetValue = i.Value;
                    break;
                default:
                    break;
            }

            if (!isAutoAnimated) mySlider.value = inverted ? mySlider.maxValue - targetValue : targetValue;
        }
    }
}
