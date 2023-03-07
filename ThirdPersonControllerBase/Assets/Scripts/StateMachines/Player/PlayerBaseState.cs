using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    //protected means only classes that inherit this will be able to access the stateMachine
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReciever.Movement) * deltaTime);
    }

    protected void FaceTarget()
    {
        //Makes sure we have a current target
        if(stateMachine.Targeter.CurrentTarget == null) {return;}
        //if we do have a target / gets vecto pointing from us to target
        Vector3 lookPosition = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        //remove y value fo the target
        lookPosition.y = 0f;

        //Rotations run off quaternions, here is a method to convert vector 3 to quaternion
        stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
    }

    protected void ReturnToLocomotion()
    {
        if(stateMachine.Targeter.CurrentTarget != null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
