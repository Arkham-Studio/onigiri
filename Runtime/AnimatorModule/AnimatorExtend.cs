using System;
using Arkham.Onigiri.Variables;
using Arkham.Onigiri.Events;
using Sirenix.OdinInspector;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorExtend : MonoBehaviour
    {
        [Title("REFERENCES")]
        [SerializeField] private Animator myAnimator;

        [Title("STATES", "Denum change play Animator State or set Trigger with same name")]
        [SerializeField] private DenumVariable denumState;
        [EnumToggleButtons, SerializeField] private AnimatorExtendStateType denumStateMethod;

        [Title("PARAMETERS", "Variables to Animator Parameters with same name")]
        [SerializeField] private ChangeableVariablePack[] variablesParameters;

        [Title("GAME EVENTS", "GameEvents to Animator Triggers with same name")]
        [SerializeField] private GameEventPack[] gameEventsTriggers;

        //[Title("EVENTS")]
        //public UnityEvent onSkipActualState;

        [Title("ANIMATIONS EVENTS")]
        public AnimEventPack[] animEvents;


        private void OnEnable()
        {
            myAnimator = myAnimator ?? GetComponent<Animator>();

            denumState?.onChange.AddListener(PlayDenumState);
            foreach (var item in variablesParameters)
            {
                item.animatorExtend = this;
                item.parametersNameHash = Animator.StringToHash(item.variable.name);
                item.variable.onChange.AddListener(item.OnChange);
            }
            foreach (var item in gameEventsTriggers)
            {
                item.animatorExtend = this;
                item.parametersNameHash = Animator.StringToHash(item.gameEvent.name);
                item.gameEvent.RegisterDelegate(item.OnChange);
            }

            if (denumState != null) PlayDenumState();
        }

        private void OnDisable()
        {
            denumState?.onChange.RemoveListener(PlayDenumState);
            foreach (var item in variablesParameters)
                item.variable.onChange.RemoveListener(item.OnChange);
            foreach (var item in gameEventsTriggers)
                item.gameEvent.UnRegisterDelegate(item.OnChange);
        }


        //  METHODS
        public void SkipActualState()
        {
            myAnimator.Play(myAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0.99f);
            //onSkipActualState.Invoke();
        }

        public void PlayDenumState()
        {
            if (denumStateMethod == AnimatorExtendStateType.States)
                myAnimator.Play(denumState.Value.name, 0, 0f);
            else
                myAnimator.SetTrigger(denumState.Value.name);
        }

        public void SetUpdateMode(int _mode) => myAnimator.updateMode = (AnimatorUpdateMode)_mode;

        //  parameters
        public void SetBool(BoolVariable v) => myAnimator.SetBool(v.name, v.Value);

        public void SetInt(IntVariable v) => myAnimator.SetInteger(v.name, v.Value);

        public void SetFloat(FloatVariable v) => myAnimator.SetFloat(v.name, v.Value);

        public void TriggerEventByIndex(int i)
        {
            if (i < 0 || i >= animEvents.Length) return;
            animEvents[i].response.Invoke();
        }

        public void TriggerEventByName(string s)
        {
            if (s == "") return;
            foreach (AnimEventPack item in animEvents)
            {
                if (item.animEventName != s) continue;
                item.response.Invoke();
            }

        }

        //  TODO
        //  copy layer
        public AnimatorControllerLayer CopyAnimatorLayer(AnimatorControllerLayer _layer)
        {
            AnimatorControllerLayer _copy = new AnimatorControllerLayer();

            _copy.avatarMask = _layer.avatarMask;
            _copy.blendingMode = _layer.blendingMode;
            _copy.defaultWeight = _layer.defaultWeight;
            _copy.name = _layer.name;

            var _copyStateMachine = _copy.stateMachine;
            var _stateMachine = _layer.stateMachine;

            //_copyStateMachine.states = new ChildAnimatorState[_layer.stateMachine.states.Length];
            for (int i = 0; i < _stateMachine.states.Length; i++)
            {
                var _state = _copyStateMachine.AddState(_stateMachine.states[i].state.name);
                _state.motion = _stateMachine.states[i].state.motion;

                foreach (var _item in _stateMachine.states[i].state.transitions)
                {
                    _state.AddTransition(_item);
                }



            }

            //for (int i = 0; i < _layer.; i++)
            //{

            //}


            return _copy;
        }


        //  UTILS
        [Serializable]
        public class ChangeableVariablePack
        {
            [HideLabel]
            public ChangeableVariable variable;

            [HideInInspector]
            public int parametersNameHash;

            [HideInInspector]
            public AnimatorExtend animatorExtend;

            public void OnChange()
            {

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
                    default:
                        animatorExtend.myAnimator.SetTrigger(variable.name);
                        break;
                }
            }
        }

        [Serializable]
        public class GameEventPack
        {
            [HideLabel]
            public GameEvent gameEvent;

            [HideInInspector]
            public int parametersNameHash;

            [HideInInspector]
            public AnimatorExtend animatorExtend;

            public void OnChange()
            {
                animatorExtend.myAnimator.SetTrigger(parametersNameHash);
            }
        }

        public enum AnimatorExtendStateType { States, Triggers }

        [Serializable]
        public class AnimEventPack
        {
            public string animEventName;
            public UnityEvent response;
        }
    }
}