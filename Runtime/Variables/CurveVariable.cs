using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/AnimationCurve")]
    public class CurveVariable : BaseVariable<AnimationCurve>
    {

        [Title("OUTPUT EVAL")]
        public FloatReference input;
        public FloatVariable output;
        public UnityEvent response;

        public void EvaluateOutput()
        {
            output.SetValue(Value.Evaluate(input.Value));
            response?.Invoke();
        }


#if UNITY_EDITOR

        [Button(ButtonSizes.Large), HorizontalGroup("Buttons")]
        public void RandomizeCurve()
        {
            Keyframe[] _keyframes = new Keyframe[50];
            DefaultValue.keys = _keyframes;
            float _offset = 1f / (_keyframes.Length * 1f);
            for (int i = 0; i < _keyframes.Length; i++)
            {
                _keyframes[i].time = i * _offset;
                _keyframes[i].value = Random.Range(0f, 1f);
                AnimationUtility.SetKeyLeftTangentMode(Value, i, AnimationUtility.TangentMode.Free);
                AnimationUtility.SetKeyRightTangentMode(Value, i, AnimationUtility.TangentMode.Free);
            }
            DefaultValue.keys = _keyframes;
        } 
#endif

    }
}
