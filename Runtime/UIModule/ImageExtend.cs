using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageExtend : MonoBehaviour
{
    [SerializeField] private Image myImage;

    private void Start()
    {
        if (myImage == null) myImage = GetComponent<Image>();
    }

    public void SetAlpha(FloatVariable _v) => SetAlpha(_v.Value);

    public void SetAlpha(float _v) => myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, _v);

    public void SetColor(ColorVariable _c) => myImage.color = _c.Value;
}
