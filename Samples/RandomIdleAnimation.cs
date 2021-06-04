using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : StateMachineBehaviour
{
    //This is the parameter name that will be set to random integer value. you must add this into your
    //animator's parameter list
    public string parameterName;

    //Minimum Generated Value
    public int minValue = 0;

    //Maximum Generated Value
    public int maxValue = 0;

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        int value = Random.Range(minValue, maxValue);
        animator.SetInteger(parameterName,value);
    }
}
