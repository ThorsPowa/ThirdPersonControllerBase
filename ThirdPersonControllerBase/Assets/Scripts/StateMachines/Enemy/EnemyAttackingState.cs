using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{

    private readonly int AttackHash = Animator.StringToHash("Attack");
    private const float TransitionDurration = 0.1f;
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        FacePlayer();
        
        stateMachine.WeaponRight.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
        stateMachine.WeaponLeft.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);

        stateMachine.Animator.CrossFadeInFixedTime(AttackHash,TransitionDurration);
    }

    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(stateMachine.Animator, "Attack") >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }   

    }

    public override void Exit()
    {
    }


}
