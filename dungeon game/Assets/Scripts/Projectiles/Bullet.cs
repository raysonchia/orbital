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
    public WeaponScriptableObject weaponData;
    public bool isEnemyProjectile;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        if (!isEnemyProjectile)
            damage = weaponData.Damage;
        
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
        if (isEnemyProjectile)
        {
            Debug.Log("enemy shooting  " + collision.name);
            if (collision.GetComponent<PlayerHealth>() != null)
            {
                collision.GetComponent<PlayerHealth>().GetHit(gameObject);
            }

            DisableObject();

        } else
        {
            Debug.Log("collide " + collision.name);

            if (collision.GetComponent<EnemyReceiveDamage>() != null)
            {
                collision.GetComponent<EnemyReceiveDamage>().DealDamage(gameObject, damage);
            }

            DisableObject();
        }
    }
}
