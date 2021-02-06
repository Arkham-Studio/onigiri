using UnityEngine;
using UnityEngine.Animations;

public class AnimatorExtendStateBehavior : StateMachineBehaviour
{
    protected AnimatorStateInfo myAnimatorStateInfos;
    protected int myLayerIndex;

    public Animator myAnimator;
    //public Animator Value
    //{
    //    get { return myAnimator; }
    //    set { myAnimator = value; }
    //}

    //public void SetValue(Animator value)
    //{
    //    Value = value;
    //}

    public AnimatorVariable variable;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateEnter(animator, stateInfo, layerIndex);
        //SetValue(animator);

        variable = new AnimatorVariable();
        variable.SetValue(animator);

        //myAnimatorStateInfos = stateInfo;
        //myLayerIndex = layerIndex;

        Debug.Log(GetInstanceID());

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        

        //if (Time.frameCount % 60 == 0)
        //{
        //    Debug.Log(myAnimator);
        //    Debug.Log(animator);
        //    SetTrigger("lol");
        //}

        //if (needToSetTrigger)
        //{
        //    if (triggerName != "") animator.SetTrigger(triggerName);
        //    needToSetTrigger = false;
        //    triggerName = "";
        //}

    }

    public void SetTrigger(string _name)
    {
        //Debug.Log("set trigger = > " + myAnimator);
        //myAnimator.SetTrigger(_name);
        variable.Value.SetTrigger(_name);

        //needToSetTrigger = true;
        //triggerName = _name;
    }

    public void SetBoolTrue(string _name)
    {

    }

    public void SetBoolFalse(string _name)
    {

    }

    public void SetStateNormalizedTime(float _value)
    {

    }

}
