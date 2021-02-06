using UnityEngine;

public class FloatVariableStateBehavior : StateMachineBehaviour
{

    [SerializeField] private FloatVariable input;

    [SerializeField] private bool isRounded;

    [SerializeField] private FloatReference minValue;
    [SerializeField] private FloatReference maxValue;

    [SerializeField] private FloatReference minOffset;
    [SerializeField] private FloatReference maxOffset = new FloatReference(1);

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (input == null) return;
        input.SetValue(CheckRounded(minValue.Value));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (input == null) return;

        float _normalizedTime = Mathf.Clamp01(Mathf.InverseLerp(minOffset.Value, maxOffset.Value, stateInfo.normalizedTime));
        input.SetValue(CheckRounded(Mathf.Lerp(minValue.Value, maxValue.Value, _normalizedTime)));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (input == null) return;
        input.SetValue(CheckRounded(maxValue.Value));
    }

    private float CheckRounded(float _v)
    {
        return isRounded ? Mathf.Round(_v) : _v;
    }

}
