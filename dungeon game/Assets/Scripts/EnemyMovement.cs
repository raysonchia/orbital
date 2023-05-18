using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;
    
    Vector2 movementDirection;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        movementDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)).normalized;
        if (movementDirection != Vector2.zero) 
        {
            bool succ = TryMove(movementDirection);
            if (!succ) {
                succ = TryMove(new Vector2(movementDirection.x, 0));
            }

            if (!succ) {
                succ = TryMove(new Vector2(0, movementDirection.y));
            }

        //     animator.SetBool("isMoving", succ);
        } else 
        {
        //     animator.SetBool("isMoving", false);
        }

        // Set direction of sprite to movement direction
        // if (movementInput.x < 0) 
        // {
        //     spriteRenderer.flipX = true;
        // } else if  (movementInput.x > 0) 
        // {
        //     spriteRenderer.flipX = false;
        // }
    }

    private bool TryMove(Vector2 direction) 
    {
        if (canMove) {
            int count = rb.Cast(
                    direction,
                    movementFilter,
                    castCollisions,
                    moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0) {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
        } 
        return false;
    }
}