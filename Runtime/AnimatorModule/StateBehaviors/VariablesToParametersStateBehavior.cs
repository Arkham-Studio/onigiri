using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Arkham.Onigiri.AnimatorModule
{
    public class VariablesToParametersStateBehavior : StateMachineBehaviour
    {
        [SerializeField, EnumToggleButtons, HideLabel] private ParametersType showEvents = 0;

        [ShowIf("IsBoolParams")]
        public BoolVariable[] boolVariables;
        [ShowIf("IsIntParams")]
        public IntVariable[] intVariables;
        [ShowIf("IsFloatParams")]
        public FloatVariable[] floatVariables;
        [ShowIf("IsTriggerParams")]
        public ChangeableVariable[] triggerVariables;

        private UnityAction[] boolEvents;
        private UnityAction[] intEvents;
        private UnityAction[] floatEvents;
        private UnityAction[] triggerEvents;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            boolEvents = new UnityAction[boolVariables.Length];
            for (int i = 0; i < boolVariables.Length; i++)
            {
                if (boolVariables[i] == null) continue;
                boolEvents[i] = () => { SetParameter(animator, boolVariables[i]); };
                boolVariables[i].onChange.AddListener(boolEvents[i]);
            }

            intEvents = new UnityAction[intVariables.Length];
            for (int i = 0; i < intVariables.Length; i++)
            {
                if (intVariables[i] == null) continue;
                intEvents[i] = () => { SetParameter(animator, intVariables[i]); };
                intVariables[i].onChange.AddListener(intEvents[i]);
            }

            floatEvents = new UnityAction[floatVariables.Length];
            for (int i = 0; i < floatVariables.Length; i++)
            {
                if (floatVariables[i] == null) continue;
                floatEvents[i] = () => { SetParameter(animator, floatVariables[i]); };
                floatVariables[i].onChange.AddListener(floatEvents[i]);
            }

            triggerEvents = new UnityAction[triggerVariables.Length];
            for (int i = 0; i < triggerVariables.Length; i++)
            {
                if (triggerVariables[i] == null) continue;
                triggerEvents[i] = () => { SetParameter(animator, triggerVariables[i]); };
                triggerVariables[i].onChange.AddListener(triggerEvents[i]);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for (int i = 0; i < boolVariables.Length; i++)
            {
                if (boolVariables[i] == null) continue;
                boolVariables[i].onChange.RemoveListener(boolEvents[i]);
            }

            for (int i = 0; i < intVariables.Length; i++)
            {
                if (intVariables[i] == null) continue;
                intVariables[i].onChange.RemoveListener(intEvents[i]);
            }

            for (int i = 0; i < floatVariables.Length; i++)
            {
                if (floatVariables[i] == null) continue;
                floatVariables[i].onChange.RemoveListener(floatEvents[i]);
            }

            for (int i = 0; i < triggerVariables.Length; i++)
            {
                if (triggerVariables[i] == null) continue;
                triggerVariables[i].onChange.RemoveListener(triggerEvents[i]);
            }
        }

        public void SetParameter<T>(Animator _animator, T _param)
        {
            switch (_param)
            {
                case BoolVariable _bool:
                    _animator.SetBool(_bool.name, _bool.Value);
                    break;
                case IntVariable _int:
                    _animator.SetInteger(_int.name, _int.Value);
                    break;
                case FloatVariable _float:
                    _animator.SetFloat(_float.name, _float.Value);
                    break;
                case StringVariable _string:
                    _animator.SetTrigger(_string.Value);
                    break;
                case ChangeableVariable _variable:
                    _animator.SetTrigger(_variable.name);
                    break;
            }
        }

        //  ODIN
        bool IsBoolParams => ((int)showEvents & (int)ParametersType.Bool) != 0;
        bool IsIntParams => ((int)showEvents & (int)ParametersType.Int) != 0;
        bool IsFloatParams => ((int)showEvents & (int)ParametersType.Float) != 0;
        bool IsTriggerParams => ((int)showEvents & (int)ParametersType.Trigger) != 0;
        [System.Flags]
        public enum ParametersType { Bool = 1 << 1, Int = 1 << 2, Float = 1 << 3, Trigger = 1 << 4, All = Bool | Int | Float | Trigger }
    }
}
