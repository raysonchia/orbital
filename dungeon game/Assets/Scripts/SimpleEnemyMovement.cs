using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    Transform player;
    public float moveSpeed;
    public EnemyScriptableObjects enemyData;
    public float spaceBetween = 0.75f;
    private GameObject[] otherEnemies;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        moveSpeed = enemyData.MoveSpeed;
        otherEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        Separate();
        //StopAtPlayer();

        // flip slug
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("attacked");
    }

    private void Separate()
    {
        foreach(GameObject e in otherEnemies)
        {
            if (e != gameObject)
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

    //private void StopAtPlayer()
    //{
    //    if (Vector3.Distance(player.position, transform.position) >= 0.75f)
    //    {
    //        Vector3 direction = player.position - transform.position;
    //        transform.Translate(direction * Time.deltaTime * moveSpeed);
    //    }
    //}

}