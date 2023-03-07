using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
//Property, you can publically get InputReader, but only privately set the InputReader
    [field: SerializeField]public InputReader InputReader{get; private set;}
    [field: SerializeField] public CharacterController Controller{get;private set;}
    [field: SerializeField] public Animator Animator {get;private set;}
    [field: SerializeField] public Targeter Targeter {get;private set;}
    [field: SerializeField] public ForceReciever ForceReciever {get;private set;}
    [field: SerializeField] public WeaponDamage WeaponLeft {get;private set;}
    [field: SerializeField] public WeaponDamage WeaponRight {get;private set;}
    [field: SerializeField] public Health Health {get;private set;}
    [field: SerializeField] public Ragdoll Ragdoll {get;private set;}
    [field: SerializeField] public LedgeDetector LedgeDetector {get;private set;}
    [field: SerializeField] public float FreeLookMovementSpeed {get;private set;}
    [field: SerializeField] public float TargetingMovementSpeed {get;private set;}
    [field: SerializeField] public float RotationDamping {get;private set;}
    [field: SerializeField] public float DodgeDuration {get;private set;}
    [field: SerializeField] public float DodgeDistance {get;private set;}
    [field: SerializeField] public float JumpForce {get;private set;}


    [field: SerializeField] public Attack[] Attacks {get;private set;}
    public float PreviousDodgeTimne {get; private set;} = Mathf.NegativeInfinity;
    public Transform MainCameraTransform {get;private set;}


    private void Start()
    {   
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
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
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDeath()
    {
        SwitchState(new PlayerDeadState(this));
    }
}

