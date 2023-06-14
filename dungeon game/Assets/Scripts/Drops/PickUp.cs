using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private float pickUpDistance = 5f;
    [SerializeField]
    private float accelerationRate = .2f;
    private float moveSpeed = 0f;
    private Vector3 moveDir;
    private Transform player;
    private Rigidbody2D rb;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 playerPos = player.position;

        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelerationRate; // acceleration effect
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            //ObjectPool.ReturnObjectToPool(gameObject);    // next time
            Destroy(gameObject);
        }
    }
}
