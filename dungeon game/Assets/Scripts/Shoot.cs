using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public float speed = 10;

    public void shootAction()
    {

        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
        

    }
}
