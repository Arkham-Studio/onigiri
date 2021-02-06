﻿using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu(menuName = "Variables/AnimationCurve")]
public class CurveVariable : BaseVariable<AnimationCurve>
{

    [Title("OUTPUT EVAL")]
    public FloatReference input;
    public FloatVariable output;
    public UnityEvent response;

    public void EvaluateOutput()
    {
        output.Value = (Value.Evaluate(input.Value));
        response?.Invoke();
    }
#if UNITY_EDITOR

    [Button(ButtonSizes.Large)]
    public void RandomizeCurve()
    {
        Keyframe[] _keyframes = new Keyframe[50];
        Value.keys = _keyframes;
        float _offset = 1f / (_keyframes.Length * 1f);
        for (int i = 0; i < _keyframes.Length; i++)
        {
            _keyframes[i].time = i * _offset;
            _keyframes[i].value = Random.Range(0f, 1f);
            AnimationUtility.SetKeyLeftTangentMode(Value, i, AnimationUtility.TangentMode.Free);
            AnimationUtility.SetKeyRightTangentMode(Value, i, AnimationUtility.TangentMode.Free);
        }
        Value.keys = _keyframes;
    } 
#endif

}
