using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int health;
    private bool isInvulnerable;

    //HealthBat Script start
    public PlayerHealthBar healthBar;
    //HealthBat Script end


    public event Action OnTakeDamage;
    public event Action OnDeath;

    public bool IsDead => health == 0;

    void Start()
    {
        health =  maxHealth;
        //HealthBat Script start
        healthBar.SetMaxHealth(maxHealth);
        //HealthBat Script end
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public void DealDamage(int damage)
    {
        if (health == 0) {return;}

        if(isInvulnerable) {return;}

        health = Mathf.Max(health - damage, 0);

        //HealthBat Script start
        healthBar.SetHealth(health);
        //HealthBat Script end

        OnTakeDamage?.Invoke();

        if(health == 0)
        {
            OnDeath?.Invoke();
        }
        
        Debug.Log(health);
    }

}
