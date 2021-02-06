using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class FloatVariableToText : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private FloatVariable value;
    [SerializeField] private bool initOnStart = true;
    [SerializeField] private string prefixe = "";
    [SerializeField] private string suffixe = "";
    [SerializeField] private string format = "0.00";
    //public float decimalRound = 100;
    [SerializeField] private float multiply = 1;

    public UnityEvent onTextChange;

    private void OnEnable() => value.onChange.AddListener(UpdateText);

    private void OnDisable() => value.onChange.RemoveListener(UpdateText);

    private void Start()
    {
        if (text == null) text = GetComponent<Text>();
        if (initOnStart) UpdateText();
    }

    [Button]
    public void UpdateText()
    {
        if (text == null || value == null) return;
        text.text = prefixe + (/*Mathf.Floor(*/(value * multiply)/* * decimalRound) * (1 / decimalRound)*/).ToString(format) + suffixe;
        onTextChange.Invoke();
    }
}
