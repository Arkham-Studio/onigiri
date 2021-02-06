using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;

public class SpriteRendererExtend : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mySpriteRenderer;

    public UnityEvent onColorChange;

    void Start()
    {
        if (mySpriteRenderer == null) mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(ColorVariable _c)
    {
        mySpriteRenderer.color = _c.Value;
        onColorChange.Invoke();
    }

    public void SetAlpha(FloatVariable _v) => SetAlpha(_v.Value);

    public void SetAlpha(float _v) => mySpriteRenderer.color = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, _v);

}
