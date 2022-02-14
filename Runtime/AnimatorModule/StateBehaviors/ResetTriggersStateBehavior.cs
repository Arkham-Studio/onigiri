using UnityEngine;

namespace Arkham.Onigiri.AnimatorModule
{
    public class ResetTriggersStateBehavior : StateMachineBehaviour
    {
        public string[] triggersName;
        private int[] triggersId;

        private void OnValidate()
        {
            if (triggersName == null) return;
            triggersId = new int[triggersName.Length];
            for (int i = 0; i < triggersName.Length; i++)
            {
                triggersId[i] = Animator.StringToHash(triggersName[i]);
            }
        }

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var item in triggersId)
            {
                animator.ResetTrigger(item);
            }
        }
    }
}
