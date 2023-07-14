using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueDoctorMovement : SimpleEnemyMovement
{
    private float attackDelay;
    private bool attackBlocked;
    private Animator animator;
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
    }

    // Update is called once per frame
    void Update()
    {
        DisableOnDeath();
        Flip();
    }

    private void FixedUpdate()
    {
        if (playerInSight() && !attackBlocked && PlayerHealth.currentHealth > 0)
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
        GameObject bomb = ObjectPool.SpawnObject(projectile, transform.position, Quaternion.identity);
        StartCoroutine(bomb.GetComponent<Bomb>().ProjectileCurveRoutine(transform.position, player.position));
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}