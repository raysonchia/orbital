using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    public float maxDistance = 20;

    private Vector2 startPosition;
    private float travelledDistance;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        GameObject playerWeapon = GameObject.Find("Weapon");
        Sprite gunSprite = playerWeapon.GetComponent<SpriteRenderer>().sprite;

        switch (gunSprite.name)
        {
            case "Base gun":
                damage = 5;
                break;
            case "Another gun":
                damage = 10;
                break;
            default:
                damage = 2;
                break;
        }
    }

    private void Update()
    {
        travelledDistance = Vector2.Distance(transform.position, startPosition);
        if (travelledDistance > maxDistance)
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        rb.velocity = Vector2.zero;
        ObjectPool.ReturnObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide " + collision.name);

        if (collision.GetComponent<EnemyReceiveDamage>() != null)
        {
            collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage);
        }

        DisableObject();
    }
}
