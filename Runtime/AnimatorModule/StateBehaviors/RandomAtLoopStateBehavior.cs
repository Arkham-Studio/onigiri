using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    public class RandomAtLoopStateBehavior : StateMachineBehaviour
    {

        [EnumToggleButtons, HideLabel] public ResponseType responseType;
        [HideIf("@responseType == ResponseType.StoreInVariable")]
        public FloatReference chance;
        [ShowIf("@responseType == ResponseType.AnimationTrigger")]
        public string triggerName;
        [ShowIf("@responseType == ResponseType.UnityEvent")]
        public UnityEvent response;
        [ShowIf("@responseType == ResponseType.StoreInVariable")]
        public FloatVariable variable;

        private bool haveLooped = false;

        public enum ResponseType { UnityEvent = 0, AnimationTrigger = 1, StoreInVariable = 2}
        

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            float _time = stateInfo.normalizedTime % 1;

            if (haveLooped && _time < 0.99f)
            {
                haveLooped = false;
            }

            if (!haveLooped && _time >= 0.98f)
            {
                haveLooped = true;
                float _v = Random.Range(0f, 1f);

                if (chance > _v && responseType != ResponseType.StoreInVariable)
                {
                    switch (responseType)
                    {
                        case ResponseType.UnityEvent:
                            response.Invoke();
                            break;
                        case ResponseType.AnimationTrigger:
                            animator.SetTrigger(triggerName);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                   if(variable != null) variable.SetValue(_v);
                }
            }
        }
    }
}
