using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private int bulletsAmount;
    private Transform player;
    public float angleOffset;

    [SerializeField]
    private float startAngle, endAngle;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    public void attackWave(float projectileSpeed, int bulletsAmount, AttackTypes type)
    {
        getAngles(type);

        float angleStep = (endAngle - startAngle) / bulletsAmount;
        Debug.Log("angle step is " + angleStep);
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bullet = ObjectPool.SpawnObject(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulDir * projectileSpeed;

            angle += angleStep;
        }
    }

    private void getAngles(AttackTypes range)
    {
        Vector3 dir = player.position - transform.position;
        angleOffset = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        angleOffset -= 90f;
        if (angleOffset < 0f)
        {
            angleOffset += 360f;
        }

        switch (range)
        {
            case AttackTypes.Circle:
                endAngle = 360f + angleOffset;
                startAngle = angleOffset;
                break;
            case AttackTypes.Half:
                endAngle = 180f + angleOffset;
                startAngle = angleOffset;
                break;
            case AttackTypes.Quarter:
                endAngle = 90f + angleOffset + 45f;
                startAngle = angleOffset + 45f;
                break;
        }
    }
}

public enum AttackTypes
{
    Circle,
    Half,
    Quarter
}


