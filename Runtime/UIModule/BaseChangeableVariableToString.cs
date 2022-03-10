using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
//using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arkham.Onigiri.UI
{
    public class BaseChangeableVariableToString<T> : MonoBehaviour
    {
        [SerializeField, PropertyOrder(0)] protected T myText;
        [SerializeField, PropertyOrder(0)] protected ChangeableVariable variable;

        protected bool initOnStart = true;
        protected bool fromVariableValue = true;
        [SerializeField] protected string prefixe = "";
        [SerializeField] protected string sufixe = "";
        [SerializeField, HorizontalGroup("params")] protected string format = "0.00";
        [SerializeField, HorizontalGroup("params")] protected float multiply = 1;


        protected string target;

        private void OnEnable() => variable.onChange.AddListener(UpdateText);

        private void OnDisable() => variable.onChange.RemoveListener(UpdateText);

        private void OnValidate()
        {
            if (myText == null)
                myText = GetComponent<T>();
        }

        private void Start()
        {
            if (myText == null)
                myText = GetComponent<T>();
            if (initOnStart)
                UpdateText();
        }

        [Button, PropertyOrder(2)]
        public void UpdateText()
        {
            if (myText == null || variable == null)
                return;

            if (fromVariableValue)
            {
                switch (variable)
                {
                    case IVariableValueTo v:
                        SetFromString(v.ValueToString());
                        break;
                    //case FloatVariable f:
                    //    SetFromFloat(f);
                    //    break;
                    //case IntVariable i:
                    //    SetFromInt(i);
                    //    break;
                    //case StringVariable s:
                    //    SetFromString(s);
                    //    break;
                    //case DenumVariable d:
                    //    SetFromDenum(d);
                    //    break;
                    //case BaseVariable<Component> bb:
                    //    SetFromBaseVariable(bb);
                    //    break;
                }
            }
            else
            {
                target = prefixe + variable.name + sufixe;
            }

            UpdateMyText();
        }


        public virtual void UpdateMyText() { }

        public virtual void SetFromFloat(FloatVariable f) => target = prefixe + ((f.Value * multiply)).ToString(format) + sufixe;
        public virtual void SetFromInt(IntVariable i) => target = prefixe + i.Value.ToString(format) + sufixe;
        public virtual void SetFromString(string s) => target = prefixe + s + sufixe;
        public virtual void SetFromString(StringVariable s) => target = prefixe + s.Value + sufixe;
        public virtual void SetFromDenum(DenumVariable d) => target = prefixe + d.Value.name + sufixe;
        public virtual void SetFromBaseVariable(BaseVariable<Component> bb) => target = prefixe + bb.Value.name + sufixe;
        public virtual void SetFromIntArray<U>(BaseArrayVariable<U> a) => target = prefixe + a.SelectedValue + sufixe;


        [HorizontalGroup("buttons"), Button("@initOnStart ? \"OnStart\" : \"OnStart\"", ButtonSizes.Small), GUIColor("@initOnStart ? Color.white : Color.grey"), PropertyOrder(1)] public void ToggleInitOnStart() => initOnStart = !initOnStart;
        [HorizontalGroup("buttons"), Button("@fromVariableValue ? \"Variable Value\" : \"Variable Name\"", ButtonSizes.Small), PropertyOrder(1)] void ToggleFromVariable() => fromVariableValue = !fromVariableValue;


    }
}
