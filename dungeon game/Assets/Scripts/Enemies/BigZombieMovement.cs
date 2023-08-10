using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigZombieMovement : SimpleEnemyMovement
{
    private float attackDelay;
    private bool attackBlocked, isSlamming;
    private float projectileSpeed;
    private float range;
    private Animator animator;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private GameObject aim;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseEnemy();
        attackDelay = enemyData.AttackRate;
        projectileSpeed = enemyData.ProjectileSpeed;
        range = enemyData.Range;
        animator = GetComponent<Animator>();
        attackBlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        DisableOnDeath();
        Flip();
    }

    private void FixedUpdate()
    {
        if (PlayerHealth.currentHealth > 0)
        {
            if (Vector3.Distance(player.position, transform.position) <= range && PlayerHealth.currentHealth > 0)
            {
                animator.SetBool("isIdle", true);

                if (attackBlocked)
                {
                    return;
                }

                StartCoroutine(AnimateAndSlam());
                attackBlocked = true;
            }
            else if (playerInSight() && !isSlamming)
            {
                animator.SetBool("isIdle", false);
                Move();
            }
        } else
        {
            animator.SetBool("isIdle", true);
        }
    }

    protected override void DisableOnDeath()
    {
        health = GetComponent<EnemyReceiveDamage>().health;
        if (health <= 0)
        {
            StopAllCoroutines();
            moveSpeed = 0f;
            gameObject.layer = LayerMask.NameToLayer("Corpse");
            GetComponent<SpriteRenderer>().sortingLayerName = "Corpse";
            this.enabled = false;
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        attackBlocked = false;
    }

    private IEnumerator AnimateAndSlam()
    {
        isSlamming = true;
        animator.SetBool("Slam", true);
        yield return StartCoroutine(Wait(1.0f));
        Slam();
        yield return StartCoroutine(Wait(1.0f));
        animator.SetBool("Slam", false);
        isSlamming = false;
    }

    private void Slam()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        aim.transform.right = direction;
        GameObject bullet = ObjectPool.SpawnObject(projectile, transform.position, aim.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
