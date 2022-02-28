using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.Module2D
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererExtend : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer mySpriteRenderer;

        void OnValidate() => mySpriteRenderer = mySpriteRenderer == null ? GetComponent<SpriteRenderer>() : mySpriteRenderer;

        public void SetColor(ColorVariable _c) => mySpriteRenderer.color = _c.Value;
        public void SetAlpha(FloatVariable _v) => SetAlpha(_v.Value);
        public void SetAlpha(float _v) => mySpriteRenderer.color = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, _v);
        public void SetSprite(SpriteVariable _s) => mySpriteRenderer.sprite = _s.Value;

    }
}
