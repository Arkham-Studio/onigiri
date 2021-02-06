using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using System;

public class AnimationClipEventsStateBehavior : StateMachineBehaviour
{

    //[SerializeField] private EventPack[] eventPacks;

    //[ValueDropdown("myValues")]
    //public float[] eventFrame;

    //public float[] myValues()
    //{
    //    var _y = (AnimatorController.FindStateMachineBehaviourContext(this)[0].animatorObject as AnimatorState).motion as AnimationClip;

    //    float[] _i = new float[_y.events.Length];
    //    for (int i = 0; i < _y.events.Length; i++)
    //    {
    //        _i[i] = _y.events[i].time;
    //    }
    //    return _i;
    //}


    //[Serializable]
    //public class EventPack
    //{

    //    public UnityEvent response;


    //}

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
           
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
