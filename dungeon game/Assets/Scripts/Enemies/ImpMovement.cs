using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpMovement : SimpleEnemyMovement
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseEnemy();
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
        if (playerInSight() && PlayerHealth.currentHealth > 0)
        {
            Move();
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Idle", true);
        }
    }
}