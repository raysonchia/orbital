using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugMovement : MonoBehaviour
{
    Transform player;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

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
}
