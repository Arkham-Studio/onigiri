using UnityEngine;

public class RandomTriggerStateBehavior : StateMachineBehaviour
{

    private bool haveLooped = false;
    public float chance;
    public string triggerName;

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
