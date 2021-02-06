using System;
using System.Collections;
using System.Collections.Generic;
using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.UI;

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
