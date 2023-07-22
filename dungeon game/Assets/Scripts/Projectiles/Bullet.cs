using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    public float maxDistance = 20;

    public Vector2 startPosition;
    private float travelledDistance;
    private Rigidbody2D rb;
    public WeaponScriptableObject weaponData;
    public bool isEnemyProjectile;

    //[SerializeField]
    //private GameObject impactEffect;

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

    protected void DisableObject()
    {
        rb.velocity = Vector2.zero;
        ObjectPool.ReturnObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEnemyProjectile)
        {
            //Debug.Log("enemy shooting  " + collision.name);
            if (collision.GetComponent<PlayerHealth>() != null)
            {
                collision.GetComponent<PlayerHealth>().GetHit(gameObject);
            }

            DisableObject();

        } else
        {
            //Debug.Log("collide " + collision.name);

            if (collision.GetComponent<EnemyReceiveDamage>() != null)
            {
                collision.GetComponent<EnemyReceiveDamage>().DealDamage(gameObject, damage);
            }

            DisableObject();
        }

        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    ContactPoint2D[] contacts = new ContactPoint2D[1];
        //    collision.GetContacts(contacts);

        //    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, contacts[0].normal * new Vector3(0, 0, -1));

        //    Instantiate(impactEffect, transform.position, rotation);
        //    Debug.Log("hit wall");
        //}
    }
}
