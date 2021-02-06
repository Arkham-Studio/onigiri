using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatVariableAnimation : MonoBehaviour
{
    public float floatValue;
    private float lastFloatValue;

    [SerializeField] private FloatVariable floatVariable;

    private void Update()
    {
        if (floatValue == lastFloatValue) return;
        floatVariable.SetValue(floatValue);
        lastFloatValue = floatValue;
    }
}
