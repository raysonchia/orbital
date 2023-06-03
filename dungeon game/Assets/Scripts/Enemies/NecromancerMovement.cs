using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerMovement : SimpleEnemyMovement
{
    private float attackDelay;
    private bool attackBlocked;
    public GameObject projectile;
    public float projectileSpeed;
    public float range = 6.5f;
    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseEnemy();
        attackDelay = enemyData.AttackRate;
        projectileSpeed = enemyData.ProjectileSpeed;
        attackBlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        DisableOnDeath();
        Flip();

        if (Vector3.Distance(player.position, transform.position) <= range && PlayerHealth.currentHealth > 0)
        {
            
            if (attackBlocked)
            {
                return;
            }

            Shoot();
            StartCoroutine(DelayAttack());
            attackBlocked = true;
        } else if (playerInSight())
        {
            Move();
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        attackBlocked = false;
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPool.SpawnObject(projectile, transform.position, Quaternion.identity);
        Vector2 direction = (player.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
