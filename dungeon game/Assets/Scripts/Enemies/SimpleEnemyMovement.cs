using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleEnemyMovement : MonoBehaviour
{
    protected Transform player;
    public float moveSpeed;
    public EnemyScriptableObjects enemyData;
    public float spaceBetween = 0.75f;
    public float lineOfSight = 12f;
    public UnityEvent<GameObject> OnCollideWithReference;
    private GameObject[] otherEnemies;


    //private float collideDelay = 1.2f;
    private float collisionOffset = 0f;
    public Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInSight())
        {
            Move();
        }
        Separate();

        Flip();
    }

    protected void InitialiseEnemy()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        moveSpeed = enemyData.MoveSpeed;
        otherEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "PlayerProjectile")
        //{
        //    Vector2 difference = (transform.position - collision.transform.position).normalized;
        //    Vector2 force = difference * 2;
        //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //    rb.AddForce(force, ForceMode2D.Impulse);

        //}

        if (collision.gameObject.tag == "Player")
        {
            //collideBlocked = true;
            PlayerHealth health;
            if (health = collision.gameObject.GetComponent<PlayerHealth>())
            {
                health.GetHit(transform.gameObject, true);
            }
            Attack();
            //StartCoroutine(CollideCooldown());
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth health;
            if (health = collision.gameObject.GetComponent<PlayerHealth>())
            {
                health.GetHit(transform.gameObject, true);
            }
            Attack();
        }

    }

    protected virtual void Attack()
    {
        Debug.Log(gameObject.name + " collision");
    }

    protected void Separate()
    {
        foreach(GameObject e in otherEnemies)
        {
            if (e != gameObject && e != null)
            {
                float distance = Vector3.Distance(e.transform.position, transform.position);
                if (distance <= spaceBetween)
                {
                    Vector3 direction = transform.position - e.transform.position;
                    transform.Translate(direction * Time.deltaTime);
                }
            }
        }
    }

    protected void Move()
    {
        if (PlayerHealth.currentHealth > 0)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

            //bool success = TryMove(direction);
            //if (!success) {
            //    success = TryMove(new Vector2(direction.x, 0));
            //}

            //if (!success) {
            //    success = TryMove(new Vector2(0, direction.y));
            //}
        }
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

    protected void Flip()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected bool playerInSight()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance <= lineOfSight)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }

    //private void StopAtPlayer()
    //{
    //    if (Vector3.Distance(player.position, transform.position) >= 0.75f)
    //    {
    //        Vector3 direction = player.position - transform.position;
    //        transform.Translate(direction * Time.deltaTime * moveSpeed);
    //    }
    //}

    // prob dont need
    //private IEnumerator CollideCooldown()
    //{
    //    yield return new WaitForSeconds(collideDelay);
    //}
}