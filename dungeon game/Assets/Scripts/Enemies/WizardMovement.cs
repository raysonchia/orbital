using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : SimpleEnemyMovement
{
    private float attackDelay;
    private bool attackBlocked;
    public GameObject projectile;
    public float projectileSpeed;
    public float range = 6.5f;
    private EnemyAttacks attacks;
    [SerializeField]
    private GameObject bossHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseEnemy();
        projectileSpeed = enemyData.ProjectileSpeed;
        attackBlocked = false;
        attacks = GetComponent<EnemyAttacks>();
        bossHealth.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisableOnDeath();
        Flip();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(player.position, transform.position) <= range && PlayerHealth.currentHealth > 0)
        {
            bossHealth.SetActive(true); // change it to scene/room trigger next time
            if (attackBlocked)
            {
                return;
            }

            Shoot();
            //StartCoroutine(DelayAttack());
            attackBlocked = true;
        }
        else if (playerInSight())
        {
            bossHealth.SetActive(true);
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
        int random = Random.Range(1, 3);

        if (random == 1)
        {
            attackDelay = 7f;
            StartCoroutine(DelayAttack());
            attacks.Wave(projectileSpeed, 15, WaveTypes.Circle, 3, 2f);
        } else
        {
            attackDelay = 5f;
            StartCoroutine(DelayAttack());
            attacks.RapidFire(projectileSpeed, 10);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
