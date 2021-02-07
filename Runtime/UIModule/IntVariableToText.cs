using System.Collections;
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Arkham.Onigiri.UI
{
    public class IntVariableToText : MonoBehaviour
    {
        public Text text;
        public IntVariable value;
        public string prefixe = "";
        public string sufixe = "";
        public string format = "0";
        public bool initOnStart = true;

        public float delayedTime = 0;

        private int lastValue = 0;

        private void OnEnable()
        {
            value.onChange.AddListener(UpdateText);
            if (initOnStart)
                UpdateText();
        }

        private void OnDisable()
        {
            value.onChange.RemoveListener(UpdateText);
            lastValue = 0;
            StopAllCoroutines();
        }

        [Button]
        public void UpdateText()
        {
            if (text == null || value == null) return;


            if (delayedTime == 0)
            {
                text.text = prefixe + value.Value.ToString(format) + sufixe;
                lastValue = value.Value;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(LerpValue());
            }


        }

        IEnumerator LerpValue()
        {
            float _actual = lastValue;
            var _target = value.Value;
            var _dif = _target - lastValue;
            var _dir = Mathf.Sign(_dif);
            var _offset = (Mathf.Abs(_dif) / (delayedTime / 0.1f)) * _dir;


            float _time = 0;
            while (_time <= delayedTime)
            {
                _actual += _offset;
                _actual = _dif > 0 ? Mathf.Clamp(_actual, lastValue, _target) : Mathf.Clamp(_actual, _target, lastValue);


                text.text = prefixe + ((int)_actual).ToString(format) + sufixe;
                _time += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            lastValue = _target;
            text.text = prefixe + (_target).ToString(format) + sufixe;
        }
    }
}
