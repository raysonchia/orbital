using UnityEngine;
using System.Collections;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField]
    private int step = 10;
    private GameObject projectile;
    public WeaponScriptableObject weaponData;
    private float nextFire;
    private float fireRate;
    private float speed;

    private void Start()
    {
        fireRate = weaponData.FireRate;
        speed = weaponData.Speed;
        projectile = weaponData.ProjectilePrefab;
    }

    public void BasicShooting()
    {

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            // GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
            GameObject bullet = ObjectPool.SpawnObject(projectile, transform.position, transform.rotation);

            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * speed, ForceMode2D.Impulse);
        }
    }

    public void Shotty(int projectilesAmount)
    {
        if (Time.time > nextFire)
        {
            gameObject.transform.Rotate(0, 0, GetStartingAngle(projectilesAmount));
            //CalculateAngles(projectilesAmount);
            nextFire = Time.time + fireRate;
            for (int i = 0; i < projectilesAmount; i++)
            {
                GameObject bullet = ObjectPool.SpawnObject(projectile, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * speed, ForceMode2D.Impulse);
                gameObject.transform.Rotate(0f, 0f, step);
            }
            gameObject.transform.localRotation = Quaternion.identity;
        }
    }

    private int GetStartingAngle(int value)
    {
        int halfValue = Mathf.FloorToInt(value / 2f);

        // to centralise when given even number of projectiles
        int offset = (value % 2 == 0) ? 5 : 0;
        return -halfValue * step + offset;
    }
}
