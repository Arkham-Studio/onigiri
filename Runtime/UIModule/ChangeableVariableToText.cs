using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ChangeableVariableToText : MonoBehaviour
{
    [SerializeField] private Text myText;
    [SerializeField] private ChangeableVariable variable;
    public ChangeableVariable Variable
    {
        set { variable = value; }
    }
    [SerializeField] private bool initOnStart = true;
    [SerializeField] private string prefixe = "";
    [SerializeField] private string sufixe = "";
    [SerializeField] private string format = "0.00";
    //public float decimalRound = 100;
    [SerializeField] private float multiply = 1;
    [SerializeField] private bool isValue = true;

    private void OnEnable() => variable.onChange.AddListener(UpdateText);

    private void OnDisable() => variable.onChange.RemoveListener(UpdateText);

    private void Start()
    {
        if (myText == null) myText = GetComponent<Text>();
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
                    myText.text = prefixe + ((f.Value * multiply)).ToString(format) + sufixe;
                    break;
                case IntVariable i:
                    myText.text = prefixe + i.Value.ToString(format) + sufixe;
                    break;
                case StringVariable s:
                    myText.text = prefixe + s.Value + sufixe;
                    break;
                case DenumVariable d:
                    myText.text = prefixe + d.Value.name + sufixe;
                    break;
                case BaseVariable<Component> bb:
                    myText.text = prefixe + bb.Value.name + sufixe;
                    break;
            }
        }
        else
        {
            myText.text = variable.name;
        }
    }


}
