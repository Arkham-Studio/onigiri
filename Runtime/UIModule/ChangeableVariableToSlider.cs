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

        public void UpdateValue()
        {
            switch (variable)
            {
                case FloatVariable f:
                    mySlider.value = f.Value;
                    break;
                case IntVariable i:
                    mySlider.value = i.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
