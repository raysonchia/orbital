using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacodaemonMovement : SimpleEnemyMovement
{
    private float attackDelay;
    private bool attackBlocked, isDashing;
    [SerializeField]
    private GameObject circleProjectile, spawnProjectile;
    [SerializeField]
    private TrailRenderer trailRenderer;
    private EnemyAttacks attacks;
    private Animator animator;

    public float projectileSpeed;
    public float range;
    public bool rapidFire;

    [SerializeField]
    private GameObject bossHealth, winUI;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseEnemy();
        projectileSpeed = enemyData.ProjectileSpeed;
        range = enemyData.Range;
        attackBlocked = false;
        rapidFire = false;
        attacks = GetComponent<EnemyAttacks>();
        animator = GetComponent<Animator>();
        bossHealth.SetActive(false);
        winUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisableOnDeath();
        Flip();
    }

    protected override void DisableOnDeath()
    {
        health = GetComponent<EnemyReceiveDamage>().health;
        if (health <= 0)
        {
            attacks.StopAllCoroutines(); // stop attacks
            StopAllCoroutines();
            moveSpeed = 0f;
            winUI.SetActive(true);
            gameObject.layer = LayerMask.NameToLayer("Corpse");
            GetComponent<SpriteRenderer>().sortingLayerName = "Corpse";
            this.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        // Move when player in sight, or when wizard is using rapid fire
        if (playerInSight() && !attackBlocked || rapidFire && attackBlocked || isDashing)
        {
            bossHealth.SetActive(true);
            Move();
        }

        if (Vector3.Distance(player.position, transform.position) <= range && PlayerHealth.currentHealth > 0)
        {
            bossHealth.SetActive(true); // change it to scene/room trigger next time
            if (attackBlocked)
            {
                return;
            }

            attackBlocked = true;
            StartCoroutine(ShootRoutine());            
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        attackBlocked = false;
        rapidFire = false;
    }

    private IEnumerator ShootRoutine()
    {
        int random = Random.Range(1, 4);

        if (random == 1)
        {
            animator.Play("Attack");
            yield return DoubleSpiralRoutine();
            yield return AttackCompleteRoutine();
        } else if (random == 2)
        {
            animator.Play("Attack");
            yield return WaveRoutine();
            yield return AttackCompleteRoutine();
        } else
        {
            moveSpeed *= 4.5f;
            trailRenderer.emitting = true;
            animator.Play("Attack");
            yield return DashRoutine();
            yield return AttackCompleteRoutine();
        }

    }

    private IEnumerator DoubleSpiralRoutine()
    {
        attackDelay = 7f;
        StartCoroutine(DelayAttack());
        attacks.Projectile = circleProjectile;
        attacks.DoubleSpiral(projectileSpeed, 5f, 1);
        yield return new WaitForSeconds(5f);
    }

    private IEnumerator WaveRoutine()
    {
        rapidFire = true;
        attackDelay = 3f;
        StartCoroutine(DelayAttack());
        attacks.Projectile = spawnProjectile;
        attacks.Wave(projectileSpeed, 2, WaveTypes.Quarter, 1, 0f);
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator DashRoutine()
    {
        attackDelay = 3.5f;
        isDashing = true;
        StartCoroutine(DelayAttack());
        yield return new WaitForSeconds(2.5f);
        isDashing = false;
        moveSpeed /= 4.5f;
        trailRenderer.emitting = false;
    }

    private IEnumerator AttackCompleteRoutine()
    {
        animator.Play("Idle");
        rapidFire = false;
        yield return null;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
