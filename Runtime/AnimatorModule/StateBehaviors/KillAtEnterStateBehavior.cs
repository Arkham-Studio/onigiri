using Arkham.Onigiri.PoolsModule;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Arkham.Onigiri.AnimatorModule
{
    public class KillAtEnterStateBehavior : StateMachineBehaviour
    {
        [EnumToggleButtons]
        [HideLabel]
        public UsePool usePoolManager;

        [ShowIf("usePoolManager", UsePool.usePoolManager)]
        public PoolManager poolManager;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (poolManager != null)
                poolManager.ReturnObject(animator.gameObject);
            else
                Destroy(animator.gameObject);
        }

        public enum UsePool { None, usePoolManager }
    }
}
