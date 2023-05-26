using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    protected Transform player;
    public float moveSpeed;
    public EnemyScriptableObjects enemyData;
    public float spaceBetween = 0.75f;
    private GameObject[] otherEnemies;

    private float collideDelay = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseEnemy();
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        Separate();
        //StopAtPlayer();

        Flip();
    }

    protected void InitialiseEnemy()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        moveSpeed = enemyData.MoveSpeed;
        otherEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collideBlocked = true;
            PlayerHealth health;
            if (health = collision.gameObject.GetComponent<PlayerHealth>())
            {
                health.GetHit(transform.gameObject);
            }
            Attack();
            //StartCoroutine(CollideCooldown());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            //Vector2 difference = (transform.position - collision.transform.position).normalized;
            //Vector2 force = difference * 2;
            //Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //rb.AddForce(force, ForceMode2D.Impulse);

        }
    }

    // prob dont need
    private IEnumerator CollideCooldown()
    {
        yield return new WaitForSeconds(collideDelay);
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
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
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

    //private void StopAtPlayer()
    //{
    //    if (Vector3.Distance(player.position, transform.position) >= 0.75f)
    //    {
    //        Vector3 direction = player.position - transform.position;
    //        transform.Translate(direction * Time.deltaTime * moveSpeed);
    //    }
    //}

}