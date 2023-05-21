using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public WeaponScriptableObject weaponData;
    private float nextFire;
    private float fireRate;
    private float speed;

    private void Start()
    {
        fireRate = weaponData.FireRate;
        speed = weaponData.Speed;
    }
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
