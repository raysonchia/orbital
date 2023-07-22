
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Bullet
{
    [SerializeField]
    private GameObject spawnObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("enemy shooting  " + collision.name);
        if (collision.GetComponent<PlayerHealth>() != null)
        {
            collision.GetComponent<PlayerHealth>().GetHit(gameObject);
        }

        DisableObject();
        ObjectPool.SpawnObject(spawnObject, transform.position, Quaternion.identity);
    }
}