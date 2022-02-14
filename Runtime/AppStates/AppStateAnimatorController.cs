#pragma warning disable CS0649
using System;
using Arkham.Onigiri.Variables;
using Arkham.Onigiri.Events;
using Sirenix.OdinInspector;
using UnityEngine.Animations;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    [RequireComponent(typeof(Animator))]
    public class AppStateAnimatorController : MonoBehaviour
    {
        [Title("REFERENCES")]
        [SerializeField] private Animator myAnimator;

        [Title("STATES", "DenumVariable onChange play Animator State with same Denum name")]
        [SerializeField] private DenumVariable denumState;

        [Title("PARAMETERS", "Event or Variables to Animator Parameters with same name and value type (IntVariable => int Params)")]
        [SerializeField] private AppStateAnimatorParametersPack[] parametersPack;


        private void OnEnable()
        {
            myAnimator = myAnimator != null ? myAnimator : GetComponent<Animator>();

            foreach (var item in parametersPack)
            {
                item.animatorExtend = this;
                item.parametersNameHash = Animator.StringToHash(item.type == AppStateAnimatorParametersPack.AppStateAnimatorParametersType.ChangeableVariable ? item.variable.name : item.gameEvent.name);
                if (item.type == AppStateAnimatorParametersPack.AppStateAnimatorParametersType.ChangeableVariable)
                    item.variable.onChange.AddListener(item.OnChange);
                else
                    item.gameEvent.RegisterDelegate(item.OnChange);
            }

            if (denumState != null)
            {
                denumState.onChange.AddListener(PlayDenumState);
                PlayDenumState();
            }
        }

        private void OnDisable()
        {
            denumState?.onChange.RemoveListener(PlayDenumState);
            foreach (var item in parametersPack)
            {
                if (item.type == AppStateAnimatorParametersPack.AppStateAnimatorParametersType.ChangeableVariable)
                    item.variable.onChange.RemoveListener(item.OnChange);
                else
                    item.gameEvent.UnRegisterDelegate(item.OnChange);
            }
        }

        public void PlayDenumState() => myAnimator.Play(denumState.Value.name, 0, 0f);

        //  UTILS
        [Serializable]
        public class AppStateAnimatorParametersPack
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
            public AppStateAnimatorController animatorExtend;

            public void OnChange()
            {
                if (type == AppStateAnimatorParametersType.GameEvent) animatorExtend.myAnimator.SetTrigger(parametersNameHash);
                else
                    switch (variable)
                    {
                        case BoolVariable b:
                            animatorExtend.myAnimator.SetBool(parametersNameHash, b.Value);
                            break;
                        case IntVariable i:
                            animatorExtend.myAnimator.SetInteger(parametersNameHash, i.Value);
                            break;
                        case FloatVariable f:
                            animatorExtend.myAnimator.SetFloat(parametersNameHash, f.Value);
                            break;
                        case StringVariable s:
                            animatorExtend.myAnimator.SetTrigger(s.Value);
                            break;
                        default:
                            animatorExtend.myAnimator.SetTrigger(variable.name);
                            break;
                    }
            }
        }
    }
}