using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : SimpleEnemyMovement
{
    private float attackDelay;
    private bool attackBlocked;
    [SerializeField]
    private GameObject circleProjectile, rapidProjectile;
    public float projectileSpeed;
    public float range;
    private EnemyAttacks attacks;
    private Animator animator;
    public bool rapidFire;

    [SerializeField]
    private GameObject bossHealth;

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
            this.enabled = false;
            attacks.StopAllCoroutines(); // stop attacks
            StopAllCoroutines();
            gameObject.layer = LayerMask.NameToLayer("Corpse");
            GetComponent<SpriteRenderer>().sortingLayerName = "Corpse";
        }
    }

    private void FixedUpdate()
    {
        // Move when player in sight, or when wizard is using rapid fire
        if (playerInSight() && !attackBlocked || rapidFire && attackBlocked)
        {
            bossHealth.SetActive(true);
            Move();
        }

        // Move till wizard is in range of player, then attack
        // 1) Circle, wizard stops and shoots a few bursts
        // 2) Continuous shooting, wizard will move and shoot
        // Theres a short delay between each attack controlled by attackDelay - Wait
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
        int random = Random.Range(1, 3);

        if (random == 1)
        {
            animator.Play("Circle");
            yield return CircleRoutine();
            yield return AttackCompleteRoutine();
        } else
        {
            animator.Play("RapidFire");
            yield return RFRoutine();
            yield return AttackCompleteRoutine();
        }

    }

    private IEnumerator CircleRoutine()
    {
        attackDelay = 7f;
        StartCoroutine(DelayAttack());
        attacks.Projectile = circleProjectile;
        attacks.Wave(projectileSpeed, 16, WaveTypes.Circle, 3, 2f);
        yield return new WaitForSeconds(5);
    }

    private IEnumerator RFRoutine()
    {
        rapidFire = true;
        attackDelay = 5f;
        StartCoroutine(DelayAttack());
        attacks.Projectile = rapidProjectile;
        attacks.RapidFire(projectileSpeed, 10);
        yield return new WaitForSeconds(3);
    }

    private IEnumerator AttackCompleteRoutine()
    {
        animator.Play("WizardWalk+Idle");
        rapidFire = false;
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
