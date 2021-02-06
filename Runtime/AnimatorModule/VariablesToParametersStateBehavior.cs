using UnityEngine;
using UnityEngine.Events;

public class VariablesToParametersStateBehavior : StateMachineBehaviour
{
    public BoolVariable[] boolVariables;
    public IntVariable[] intVariables;
    public FloatVariable[] floatVariables;
    public StringVariable[] triggerVariables;

    private UnityAction[] boolEvents;
    private UnityAction[] intEvents;
    private UnityAction[] floatEvents;
    private UnityAction[] triggerEvents;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boolEvents = new UnityAction[boolVariables.Length];
        for (int i = 0; i < boolVariables.Length; i++)
        {
            boolEvents[i] = () => { SetParameter(animator, boolVariables[i]); };
            boolVariables[i].onChange.AddListener(boolEvents[i]);
        }

        intEvents = new UnityAction[intVariables.Length];
        for (int i = 0; i < intVariables.Length; i++)
        {
            intEvents[i] = () => { SetParameter(animator, intVariables[i]); };
            intVariables[i].onChange.AddListener(intEvents[i]);
        }

        floatEvents = new UnityAction[floatVariables.Length];
        for (int i = 0; i < floatVariables.Length; i++)
        {
            floatEvents[i] = () => { SetParameter(animator, floatVariables[i]); };
            floatVariables[i].onChange.AddListener(floatEvents[i]);
        }

        triggerEvents = new UnityAction[triggerVariables.Length];
        Debug.Log(triggerVariables.Length);
        Debug.Log(triggerEvents.Length);
        for (int i = 0; i < triggerVariables.Length; i++)
        {
            Debug.Log(i); 
            triggerEvents[i] = () => { SetParameter(animator, triggerVariables[i]); };
            triggerVariables[i].onChange.AddListener(triggerEvents[i]);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < boolVariables.Length; i++)
            boolVariables[i].onChange.RemoveListener(boolEvents[i]);

        for (int i = 0; i < intVariables.Length; i++)
            intVariables[i].onChange.RemoveListener(intEvents[i]);

        for (int i = 0; i < floatVariables.Length; i++)
            floatVariables[i].onChange.RemoveListener(floatEvents[i]);

        for (int i = 0; i < triggerVariables.Length; i++)
            triggerVariables[i].onChange.RemoveListener(triggerEvents[i]);
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
                _animator.SetTrigger(_string.name);
                break;
        }
    }
}
