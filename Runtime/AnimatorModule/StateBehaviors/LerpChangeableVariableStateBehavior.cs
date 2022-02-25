#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    public class LerpChangeableVariableStateBehavior : StateMachineBehaviour
    {
        [SerializeField, PropertyOrder(Order = -2)] private ChangeableVariable output;

        [SerializeField, HorizontalGroup("value",Title = "Values",LabelWidth = 50), HideIf("@output != null && output.GetType() == typeof(ColorVariable)")] private FloatReference min;
        [SerializeField, HorizontalGroup("value"), HideIf("@output != null && output.GetType() == typeof(ColorVariable)")] private FloatReference max = new FloatReference(1);

        [SerializeField, HorizontalGroup("value"), ShowIf("@output != null && output.GetType() == typeof(ColorVariable)")] private ColorReference minC = new ColorReference(Color.black);
        [SerializeField, HorizontalGroup("value"), ShowIf("@output != null && output.GetType() == typeof(ColorVariable)")] private ColorReference maxC = new ColorReference(Color.white);

        [SerializeField, HorizontalGroup("time",Title = "Time (normalized)", LabelWidth = 50)] private FloatReference start;
        [SerializeField, HorizontalGroup("time")] private FloatReference end = new FloatReference(1);

        private bool isRounded;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (output == null) return;
            SetValue(0);
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (output == null) return;
            float _normalizedTime = Mathf.Clamp01(Mathf.InverseLerp(start.Value, end.Value, stateInfo.normalizedTime));
            SetValue(_normalizedTime);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (output == null) return;
            SetValue(1);
        }

        private float CheckRounded(float _v) => isRounded ? Mathf.Round(_v) : _v;

        private void SetValue(float _n)
        {
            switch (output)
            {
                case FloatVariable _f:
                    _f.SetValue(CheckRounded(Mathf.Lerp(min.Value, max.Value, _n)));
                    break;
                case IntVariable _i:
                    _i.SetValue(Mathf.RoundToInt(CheckRounded(Mathf.Lerp(min.Value, max.Value, _n))));
                    break;
                case ColorVariable _c:
                    _c.SetValue(Color.Lerp(minC.Value, maxC.Value, _n));
                    break;
                default:
                    break;
            }
        }

        [Button(ButtonSizes.Small, Name = "@isRounded ? \"Rounded\" : \"Not Rounded\""), PropertyOrder(Order = -1), HideIf("@output.GetType() == typeof(ColorVariable)")]
        void ToggleRounded() => isRounded = !isRounded;

    }
}