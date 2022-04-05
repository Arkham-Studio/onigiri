#pragma warning disable CS0649
using Arkham.Onigiri.Events;
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{

    public class AnimatorParametersController : MonoBehaviour
    {
        [Title("REFERENCES")]
        [SerializeField] private Animator myAnimator;

        [Title("PARAMETERS", "Event or Variables to Animator Parameters with same name and value type (IntVariable => int Params)")]
        [SerializeField] private ChangeableVariableToParameterPack[] parametersPack;

        private void OnEnable()
        {
            myAnimator = myAnimator != null ? myAnimator : GetComponent<Animator>();

            foreach (var item in parametersPack)
            {
                item.controller = this;
                item.parametersNameHash = Animator.StringToHash(item.type == ChangeableVariableToParameterPack.AppStateAnimatorParametersType.ChangeableVariable ? item.variable.name : item.gameEvent.name);
                if (item.type == ChangeableVariableToParameterPack.AppStateAnimatorParametersType.ChangeableVariable)
                {
                    item.variable.onChange.AddListener(item.OnChange);
                    item.OnChange();
                }
                else
                    item.gameEvent.RegisterDelegate(item.OnChange);

               
            }

        }

        private void OnDisable()
        {
            foreach (var item in parametersPack)
            {
                if (item.type == ChangeableVariableToParameterPack.AppStateAnimatorParametersType.ChangeableVariable)
                    item.variable.onChange.RemoveListener(item.OnChange);
                else
                    item.gameEvent.UnRegisterDelegate(item.OnChange);
            }
        }

        //  UTILS
        [Serializable]
        public class ChangeableVariableToParameterPack
        {
            public enum AppStateAnimatorParametersType { ChangeableVariable, GameEvent }
            [EnumToggleButtons, HideLabel]
            public AppStateAnimatorParametersType type;

            [HideLabel, ShowIf("type", AppStateAnimatorParametersType.ChangeableVariable)]
            public ChangeableVariable variable;

            [HideLabel, ShowIf("type", AppStateAnimatorParametersType.GameEvent)]
            public GameEvent gameEvent;

            [HideInInspector]
            public int parametersNameHash;

            [HideInInspector]
            public AnimatorParametersController controller;

            public void OnChange()
            {
                if (type == AppStateAnimatorParametersType.GameEvent) controller.myAnimator.SetTrigger(parametersNameHash);
                else
                    switch (variable)
                    {
                        case BoolVariable b:
                            controller.myAnimator.SetBool(parametersNameHash, b.Value);
                            break;
                        case IntVariable i:
                            controller.myAnimator.SetInteger(parametersNameHash, i.Value);
                            break;
                        case FloatVariable f:
                            controller.myAnimator.SetFloat(parametersNameHash, f.Value);
                            break;
                        case StringVariable s:
                            controller.myAnimator.SetTrigger(s.Value);
                            break;
                        default:
                            controller.myAnimator.SetTrigger(variable.name);
                            break;
                    }
            }
        }
    }
}