using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : SimpleEnemyMovement
{
    public Animator animator;
    private float attackDelay = 1f;
    private bool attackBlocked;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        Separate();
        Flip();
        Move();

        if (Vector3.Distance(player.position, transform.position) <= 1.5f)
        {
            if (attackBlocked)
            {
                return;
            }

            Swing();
            attackBlocked = true;
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
