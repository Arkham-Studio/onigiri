using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Arkham.Onigiri.UI
{
    public class StringVariableToText : MonoBehaviour
    {
        [InfoBox("DEPRECATED - use ChangeableVariableToText", InfoMessageType.Warning)]
        public Text myText;
        //public DOTweenAnimation myDOTweenAnimation;
        public StringVariable stringVariable;
        public string prefixe = "";
        public string sufixe = "";

        public bool initOnStart = true;

        public UnityEvent onUpdateText;

        private void OnEnable()
        {
            stringVariable.onChange.AddListener(UpdateText);
            if (initOnStart) UpdateText();
        }

        private void OnDisable()
        {
            stringVariable.onChange.RemoveListener(UpdateText);
        }


        public void UpdateText()
        {
            if (myText == null || stringVariable == null) return;
            myText.text = prefixe + stringVariable.Value + sufixe;


            //if (myDOTweenAnimation != null)
            //    myDOTweenAnimation.endValueString = prefixe + stringVariable.Value + sufixe;

            onUpdateText.Invoke();
        }
    }
}
