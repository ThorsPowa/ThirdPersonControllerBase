using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
   protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
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

    protected void FacePlayer()
    {
        if(stateMachine.PlayerHealth == null) {return;}
        Vector3 lookPosition = stateMachine.PlayerHealth.transform.position - stateMachine.transform.position;
        lookPosition.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
    }


    protected bool IsInChaseRange()
    {
        if(stateMachine.PlayerHealth.IsDead) {return false;}

        float playerDistanceSquare = (stateMachine.PlayerHealth.transform.position - stateMachine.transform.position).sqrMagnitude;
        
        return playerDistanceSquare <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
    }
}
