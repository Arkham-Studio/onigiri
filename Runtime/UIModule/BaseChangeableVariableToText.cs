using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
//using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arkham.Onigiri.UI
{
    public class BaseChangeableVariableToText<T> : MonoBehaviour
    {
        [SerializeField] protected T myText;
        [SerializeField] protected ChangeableVariable variable;

        public ChangeableVariable Variable
        {
            set { variable = value; }
        }
        [SerializeField] protected bool initOnStart = true;
        [SerializeField] protected string prefixe = "";
        [SerializeField] protected string sufixe = "";
        [SerializeField] protected string format = "0.00";
        [SerializeField] protected float multiply = 1;
        [SerializeField] protected bool isValue = true;

        private void OnEnable() => variable.onChange.AddListener(UpdateText);

        private void OnDisable() => variable.onChange.RemoveListener(UpdateText);

        private void Start()
        {
            if (myText == null) myText = GetComponent<T>();
            if (initOnStart) UpdateText();
        }

        [Button]
        public void UpdateText()
        {
            if (myText == null || variable == null) return;

            if (isValue)
            {
                switch (variable)
                {
                    case FloatVariable f:
                        SetFromFloat(f);
                        break;
                    case IntVariable i:
                        SetFromInt(i);
                        break;
                    case StringVariable s:
                        SetFromString(s);
                        break;
                    case DenumVariable d:
                        SetFromDenum(d);
                        break;
                    case BaseVariable<Component> bb:
                        SetFromBaseVariable(bb);
                        break;
                }
            }
        }

        public virtual void SetFromFloat(FloatVariable f) { }
        public virtual void SetFromInt(IntVariable i) { }
        public virtual void SetFromString(StringVariable s) { }
        public virtual void SetFromDenum(DenumVariable d) { }
        public virtual void SetFromBaseVariable(BaseVariable<Component> bb) { }
    }
}
