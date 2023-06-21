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

            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * speed, ForceMode2D.Impulse);
        }
    }
}
