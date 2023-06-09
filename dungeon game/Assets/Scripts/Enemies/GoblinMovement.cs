using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : SimpleEnemyMovement
{
    public Animator animator;
    private Animator moveAnimator;
    private float attackDelay;
    private bool attackBlocked;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseEnemy();
        attackDelay = enemyData.AttackRate;
        moveAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DisableOnDeath();
        Flip();

        if (playerInSight() && PlayerHealth.currentHealth > 0)
        {
            Move();
            moveAnimator.SetBool("Idle", false);
        } else
        {
            moveAnimator.SetBool("Idle", true);
        }

        if (Vector3.Distance(player.position, transform.position) <= 1.5f && PlayerHealth.currentHealth > 0)
        {
            if (attackBlocked)
            {
                return;
            }

            Swing();
            attackBlocked = true;
        }
    }

    protected override void DisableOnDeath()
    {
        health = GetComponent<EnemyReceiveDamage>().health;
        if (health <= 0)
        {
            if (GetComponent<DropsSpawner>() != null)
            {
                GetComponent<DropsSpawner>().SpawnDrops();
            }
            this.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Corpse");
            GetComponent<SpriteRenderer>().sortingLayerName = "Corpse";
            // hide weapon
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void Swing()
    {  
        animator.SetTrigger("Attack");
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        attackBlocked = false;
    }
}
