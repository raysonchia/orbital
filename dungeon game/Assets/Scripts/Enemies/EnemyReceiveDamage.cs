using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyReceiveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public EnemyScriptableObjects enemyData;
    public UnityEvent<GameObject> OnHitWithReference;

    // Start is called before the first frame update
    void Start()
    {
        health = enemyData.MaxHealth;
        maxHealth = health;
    }

    public void DealDamage(GameObject projectile, float damage)
    {
        OnHitWithReference?.Invoke(projectile);
        health -= damage;
        CheckDeath();
    }

    private void CheckOverHeal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
