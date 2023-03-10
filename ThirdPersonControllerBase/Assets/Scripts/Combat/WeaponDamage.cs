using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    private int damage;
    private float knockback;
    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other == myCollider){return;}

        if(alreadyCollidedWith.Contains(other)){return;}

        alreadyCollidedWith.Add(other);
//Player
        if(other.TryGetComponent<PlayerHealth>(out PlayerHealth health))
        {
            health.DealDamage(damage);
        }
//Enemy
        if(other.TryGetComponent<EnemyHealth>(out EnemyHealth enemyhealth))
        {
            enemyhealth.DealDamage(damage);
        }

        if(other.TryGetComponent<ForceReciever>(out ForceReciever forceReciever))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReciever.AddForce(direction * knockback);
        }
    }   

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}

