using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using Arkham.Onigiri.Variables;
using System;

namespace Arkham.Onigiri.UI
{
    public class StringVariableTypewriter : MonoBehaviour
    {
        [SerializeField, ShowIf("componentType", ComponentType.TextMesh)] private TextMeshProUGUI myTextTMP;
        [SerializeField, ShowIf("componentType", ComponentType.Text)] private Text myText;

        [SerializeField] private StringVariable input;

        [SerializeField, HorizontalGroup("params"), LabelWidth(50)] private FloatReference timing = new FloatReference(1f);
        [SerializeField, HorizontalGroup("params")] private bool onStart = true;

        [FoldoutGroup("Events")]public UnityEvent onFinished;

        private float progress;
        private int index;
        private int lenght;

        private void OnEnable()
        {
            input.onChange.AddListener(StartTypewrite);
        }
        private void OnDisable()
        {
            input.onChange.RemoveListener(StartTypewrite);
        }

        private void Start()
        {
            if (componentType == ComponentType.Text && myText == null) myText = GetComponent<Text>();
            if (componentType == ComponentType.TextMesh && myTextTMP == null) myTextTMP = GetComponent<TextMeshProUGUI>();

            if (onStart) StartTypewrite();
        }

        [Button, ShowIf("@UnityEngine.Application.isPlaying")]
        public void StartTypewrite()
        {
            StopCoroutine("Typewrite");
            StartCoroutine("Typewrite");
        }

        IEnumerator Typewrite()
        {
            progress = 0;
            index = 0;
            lenght = input.Value.Length;

            while (index < lenght)
            {
                progress += (Time.fixedDeltaTime / timing) * lenght;
                SetValue(Mathf.FloorToInt(progress));
                yield return new WaitForFixedUpdate();
            }

            onFinished.Invoke();
        }

        private void SetValue(int _s)
        {
            index += _s;
            switch (componentType)
            {
                case ComponentType.Text:
                    myText.text = input.Value.Substring(0, index);
                    break;
                case ComponentType.TextMesh:
                    myTextTMP.text = input.Value.Substring(0, index);
                    break;
                default:
                    break;
            }
            progress -= _s;
        }

        [EnumToggleButtons, HideLabel, PropertyOrder(-3)] public ComponentType componentType;
        public enum ComponentType { Text, TextMesh }
    }
}

