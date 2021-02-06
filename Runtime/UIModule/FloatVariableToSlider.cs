using UnityEngine;
using UnityEngine.UI;

public class FloatVariableToSlider : MonoBehaviour
{

    [SerializeField] private Slider mySlider;
    [SerializeField] private FloatVariable value;
    public FloatVariable Value
    {
        set { this.value = value; }
    }
    [SerializeField] private bool initOnStart = true;
    [SerializeField] private bool listenToChange = true;

    private void OnEnable()
    {
        if (listenToChange)
            value.onChange.AddListener(UpdateValue);
    }

    private void OnDisable()
    {
        if (listenToChange)
            value.onChange.RemoveListener(UpdateValue);
    }

    void Start()
    {
        if (mySlider == null) mySlider = GetComponent<Slider>();
        if (initOnStart) UpdateValue();
    }

    public void UpdateValue() => mySlider.value = value.Value;
}
