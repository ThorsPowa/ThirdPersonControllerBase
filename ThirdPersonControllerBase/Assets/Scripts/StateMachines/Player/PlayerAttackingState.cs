using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;
    private Attack attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.WeaponLeft.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.WeaponRight.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {  
        Move(deltaTime);

        FaceTarget();

        float nomalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if(nomalizedTime >= previousFrameTime && nomalizedTime < 1f)
        {
            if(nomalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if(stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(nomalizedTime);
            }
        }
        else
        {
            //This will take us back to locomotion
            if(stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }

        previousFrameTime = nomalizedTime;
    }

    public override void Exit()
    {
        
    }

    private void TryComboAttack(float nomalizedTime)
    {
        //Make Sure we have a combo attack
        if(attack.ComboStateIndex == -1){return;}
        //Make sure we are far enough throuhg to do it 
        if(nomalizedTime < attack.ComboAttackTime){return;}
        //If we are far enough throuhg to do it we switch state to attack
        stateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                stateMachine,
                attack.ComboStateIndex
            )
        );
    }

    private void TryApplyForce()
    {
        if(alreadyAppliedForce){return;}

        stateMachine.ForceReciever.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;
    }
}
