﻿using Arkham.Onigiri.Variables;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Arkham.Onigiri.Module2D
{
    public class ChangeableVariableToSpriteRenderer : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer mySpriteRenderer;
        [SerializeField] private ChangeableVariable value;
        private bool initOnStart = true;
        private bool listenToChange = true;

        private void OnEnable()
        {
            if (listenToChange)
                value?.onChange.AddListener(UpdateValue);
        }

        private void OnDisable()
        {
            if (listenToChange)
                value?.onChange.RemoveListener(UpdateValue);
        }

        private void OnValidate()
        {
            if (mySpriteRenderer == null) mySpriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            if (initOnStart) UpdateValue();
        }

        [Button, PropertyOrder(2)]
        public void UpdateValue()
        {
            switch (value)
            {
                case ColorVariable _c:
                    mySpriteRenderer.color = _c.Value;
                    break;
                case SpriteVariable _s:
                    mySpriteRenderer.sprite = _s.Value;
                    break;
                case TextureVariable _t:
                    mySpriteRenderer.sprite = Sprite.Create((Texture2D)_t.Value, new Rect(0, 0, _t.Value.width, _t.Value.height), Vector2.one * .5f);
                    break;
                default:
                    break;
            }
        }

        [HorizontalGroup("buttons"), Button("@initOnStart ? \"OnStart\" : \"OnStart\"", ButtonSizes.Small), GUIColor("@initOnStart ? Color.white : Color.grey"), PropertyOrder(1)] void ToggleInitOnStart() => initOnStart = !initOnStart;
        [HorizontalGroup("buttons"), Button("@listenToChange ? \"OnChange\" : \"OnChange\"", ButtonSizes.Small), GUIColor("@listenToChange ? Color.white : Color.grey"), PropertyOrder(1)] void TogglelistenToChange() => listenToChange = !listenToChange;

    }
}