using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    public class RandomTriggerStateBehavior : StateMachineBehaviour
    {
        [InfoBox("Chance (0.0 - 1.0) to set Trigger at current state loop", InfoMessageType.None)]
        public string triggerName;
        public float chance;

        private bool haveLooped = false;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            float _time = stateInfo.normalizedTime % 1;

            if (haveLooped && _time < 0.99f)
            {
                haveLooped = false;
            }

            if (!haveLooped && _time > 0.98f)
            {
                haveLooped = true;
                if (chance > Random.Range(0f, 1f))
                {
                    animator.SetTrigger(triggerName);
                }
            }
        }
    }
}
