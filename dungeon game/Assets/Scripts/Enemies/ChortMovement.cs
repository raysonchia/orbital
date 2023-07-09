using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChortMovement : SimpleEnemyMovement
{
    private float attackDelay;
    private bool attackBlocked;
    private Animator animator;
    private EnemyAttacks attacks;
    public GameObject projectile;
    public float projectileSpeed;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseEnemy();
        attackDelay = enemyData.AttackRate;
        projectileSpeed = enemyData.ProjectileSpeed;
        range = enemyData.Range;
        attackBlocked = false;
        animator = GetComponent<Animator>();
        attacks = GetComponent<EnemyAttacks>();
        attacks.Projectile = projectile;
    }

    // Update is called once per frame
    void Update()
    {
        DisableOnDeath();
        Flip();
    }

    private void FixedUpdate()
    {
        if (playerInSight() && !attackBlocked)
        {
            Move();
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Idle", true);
        }

        if (Vector3.Distance(player.position, transform.position) <= range && PlayerHealth.currentHealth > 0)
        {
            if (attackBlocked)
            {
                return;
            }

            attackBlocked = true;
            Shoot();
            StartCoroutine(DelayAttack());
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        attackBlocked = false;
    }

    private void Shoot()
    {
        animator.Play("ChortAttack");
    }

    public void Waves()
    {
        attacks.Wave(projectileSpeed, 5, WaveTypes.Quarter, 1, 0f);
    }

    public void ShootComplete()
    {
        animator.Play("ChortIdle");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}