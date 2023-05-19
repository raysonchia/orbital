using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public float speed = 8;
    private float fireRate = 0.17f;
    private float nextFire;

    public void shootAction()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            // GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
            GameObject bullet = ObjectPool.SpawnObject(projectile, transform.position, transform.rotation);

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }
}
