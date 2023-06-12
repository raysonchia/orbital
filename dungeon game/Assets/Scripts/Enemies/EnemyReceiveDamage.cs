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
    private Animator animator;
    [SerializeField]
    private bool isBoss = false;
    [SerializeField]
    HealthBar bossHealth;

    void Awake()
    {
        health = enemyData.MaxHealth;
        maxHealth = health;
        animator = GetComponent<Animator>();
        if (isBoss)
        {
            bossHealth = GetComponentInChildren<HealthBar>();
        }
    }

    public void DealDamage(GameObject projectile, float damage)
    {
        if (health > 0)
        {
            OnHitWithReference?.Invoke(projectile);
            health -= damage;

            if (isBoss)
            {
                bossHealth.UpdateHealthBar(health, maxHealth);
            }
            CheckDeath();
        }
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
            animator.SetTrigger("isDead");
            StartCoroutine(WaitAndDestroy());
            
        }
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }

}
