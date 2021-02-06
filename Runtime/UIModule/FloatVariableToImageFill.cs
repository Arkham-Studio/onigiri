using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.UI;

public class FloatVariableToImageFill : MonoBehaviour
{
    [SerializeField] private Image myImage;
    [SerializeField] private FloatVariable value;
    [SerializeField] private bool initOnStart = true;

    private void OnEnable() => value.onChange.AddListener(UpdateValue);

    private void OnDisable() => value.onChange.RemoveListener(UpdateValue);

    void Start()
    {
        if (myImage == null) myImage = GetComponent<Image>();
        if (initOnStart) UpdateValue();
    }

    public void UpdateValue() => myImage.fillAmount = value.Value;
}
