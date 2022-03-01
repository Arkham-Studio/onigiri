using Arkham.Onigiri.Variables;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Arkham.Onigiri.UI
{
    public class BaseChangeableVariableToFloat<T> : MonoBehaviour where T : Component
    {
        [SerializeField, PropertyOrder(0)] protected T myComponent;
        [SerializeField, PropertyOrder(0)] protected ChangeableVariable variable;

        protected bool initOnStart = true;
        protected bool listenToChange = true;
        protected bool inverted = false;
        protected bool isAutoAnimated = false;
        [SerializeField, ShowIf("isAutoAnimated"), PropertyOrder(2)] protected float smooth = 1f;

        protected float targetValue = 0;

        private void OnEnable()
        {
            if (listenToChange && variable != null)
                variable.onChange.AddListener(UpdateValue);
        }

        private void OnDisable()
        {
            if (listenToChange && variable != null)
                variable.onChange.RemoveListener(UpdateValue);
        }

        private void OnValidate()
        {
            if (myComponent == null) myComponent = GetComponent<T>();
        }

        void Start()
        {
            myComponent = myComponent ?? GetComponent<T>();
            if (initOnStart) UpdateValue();
        }

        void FixedUpdate()
        {
            if (!isAutoAnimated) return;
            UpdateValueLerp();
        }

        public virtual void UpdateValueLerp()
        {
            //mySlider.value = Mathf.Lerp(mySlider.value, targetValue, Time.fixedDeltaTime / smooth);
        }

        [Button, PropertyOrder(3), HideIf("isAutoAnimated")]
        protected void UpdateValue()
        {
            switch (variable)
            {
                case FloatVariable f:
                    UpdateOnFloat(f);
                    break;
                case IntVariable i:
                    UpdateOnInt(i);
                    break;
                case BoolVariable b:
                    UpdateOnBool(b);
                    break;
                case Vector3Variable v:
                    UpdateOnVector3(v);
                    break;
                default:
                    break;
            }

            if (!isAutoAnimated) UpdateOnEvent();
        }


        public virtual void UpdateOnFloat(FloatVariable _v) => targetValue = _v.Value;
        public virtual void UpdateOnInt(IntVariable _v) => targetValue = _v.Value;
        public virtual void UpdateOnBool(BoolVariable _v) => targetValue = _v.Value ? 1 : 0;
        public virtual void UpdateOnVector3(Vector3Variable _v) => targetValue = _v.Value.magnitude;

        public virtual void UpdateOnEvent()
        {
            //mySlider.value = inverted ? mySlider.maxValue - targetValue : targetValue;

        }


        [HorizontalGroup("buttons"), Button("@initOnStart ? \"OnStart\" : \"OnStart\"", ButtonSizes.Small), GUIColor("@initOnStart ? Color.white : Color.grey"), PropertyOrder(1)] public void ToggleInitOnStart() => initOnStart = !initOnStart;
        [HorizontalGroup("buttons"), Button("@listenToChange ? \"OnChange\" : \"OnChange\"", ButtonSizes.Small), GUIColor("@listenToChange ? Color.white : Color.grey"), PropertyOrder(1)] void TogglelistenToChange() => listenToChange = !listenToChange;
        [HorizontalGroup("buttons"), Button("@inverted ? \"Inverted\" : \"Not Inverted\"", ButtonSizes.Small), GUIColor("@inverted ? Color.white : Color.grey"), PropertyOrder(1)] void Toggleinverted() => inverted = !inverted;
        [HorizontalGroup("buttons"), Button("@isAutoAnimated ? \"Animated\" : \"Not Animated\"", ButtonSizes.Small), GUIColor("@isAutoAnimated ? Color.white : Color.grey"), PropertyOrder(1)] void ToggleAutoAnimated() => isAutoAnimated = !isAutoAnimated;


    }
}
