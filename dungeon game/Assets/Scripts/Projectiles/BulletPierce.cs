
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPierce : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyReceiveDamage>() != null)
        {
            collision.GetComponent<EnemyReceiveDamage>().DealDamage(gameObject, damage);
        }
        else
        {
            DisableObject();
        }
    }
}