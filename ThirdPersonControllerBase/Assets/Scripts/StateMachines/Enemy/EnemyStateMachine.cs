using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator {get;private set;}
    [field: SerializeField] public CharacterController Controller {get;private set;}
    [field: SerializeField] public ForceReciever ForceReciever {get;private set;}
    [field: SerializeField] public NavMeshAgent Agent {get;private set;}
    [field: SerializeField] public WeaponDamage WeaponRight {get;private set;}
    [field: SerializeField] public WeaponDamage WeaponLeft {get;private set;}
    [field: SerializeField] public EnemyHealth Health {get;private set;}
    [field: SerializeField] public Target Target {get;private set;}
    [field: SerializeField] public Ragdoll Ragdoll {get;private set;}


    [field: SerializeField] public float MovementSpeed {get;private set;}
    [field: SerializeField] public float PlayerChasingRange {get;private set;}
    [field: SerializeField] public float PlayerAttackRange {get;private set;}
    [field: SerializeField] public int AttackDamage {get;private set;}
    [field: SerializeField] public int AttackKnockback {get;private set;}
    public PlayerHealth PlayerHealth {get; private set;}

    private void Start()
    {   
        PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        
        Agent.updatePosition = false;
        Agent.updateRotation = false;


        SwitchState(new EnemyIdleState(this));
    }

    private void OnEnable() 
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDeath += HandleDeath;
    }
    private void OnDisable() 
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDeath -= HandleDeath;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }

    private void HandleDeath()
    {
        SwitchState(new EnemyDeadState(this));
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }
}
