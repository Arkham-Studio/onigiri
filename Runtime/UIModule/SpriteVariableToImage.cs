#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Arkham.Onigiri.UI
{
    public class SpriteVariableToImage : MonoBehaviour
    {
        [SerializeField] private SpriteVariable spriteVariable;
        [SerializeField] private Image myImage;
        [SerializeField] private bool onStart = true;
        [SerializeField] private bool onChange = true;

        private void OnEnable()
        {
            myImage = myImage ?? GetComponent<Image>();
            if (onChange) spriteVariable?.onChange.AddListener(OnSpriteChange);
            if (onStart) OnSpriteChange();
        }

        private void OnDisable()
        {
            if (onChange) spriteVariable?.onChange.RemoveListener(OnSpriteChange);
        }

        private void OnSpriteChange()
        {
            myImage.sprite = spriteVariable.Value;
        }
    }
}
