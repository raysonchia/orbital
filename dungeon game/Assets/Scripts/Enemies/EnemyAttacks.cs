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

    public void Wave(float projectileSpeed, int bulletsAmount, WaveTypes type)
    {
        GetAngles(type);

        float angleStep = (endAngle - startAngle) / bulletsAmount;
        //Debug.Log("angle step is " + angleStep);
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

    private void GetAngles(WaveTypes range)
    {
        Vector2 dir = (player.position - transform.position).normalized;

        // 0 starts at N, -180/180 at S
        angleOffset = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        // doesnt matter but make it positive
        if (angleOffset < 0f)
        {
            angleOffset += 360f;
        }

        switch (range)
        {
            case WaveTypes.Circle:
                endAngle = 360f + angleOffset;  // 1 will aim player
                startAngle = angleOffset;
                break;
            case WaveTypes.Half:
                angleOffset -= 90f;     // centralise aiming to player
                endAngle = 180f + angleOffset;
                startAngle = angleOffset;
                break;
            case WaveTypes.Quarter:
                angleOffset -= 45f;     // centralise aiming
                endAngle = 90f + angleOffset;
                startAngle = angleOffset;
                break;
        }
    }

    public void RapidFire(float projectileSpeed, int bulletsAmount)
    {
        StartCoroutine(RapidFireRoutine(projectileSpeed, bulletsAmount, 0.2f));
    }

    public void RapidFire(float projectileSpeed, int bulletsAmount, float delay)
    {
        StartCoroutine(RapidFireRoutine(projectileSpeed, bulletsAmount, delay));
    }

    private IEnumerator RapidFireRoutine(float projectileSpeed, int bulletsAmount, float delay)
    {
        for (int i = 0; i < bulletsAmount; i++)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            GameObject bullet = ObjectPool.SpawnObject(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir * projectileSpeed;
            yield return new WaitForSeconds(delay);
        }
    }

    public void Spiral(float projectileSpeed, float duration)
    {
        StartCoroutine(SpiralRoutine(duration, projectileSpeed, 0.1f));
    }

    public void Spiral(float projectileSpeed, float duration, float delay)
    {
        StartCoroutine(SpiralRoutine(duration, projectileSpeed, delay));
    }

    private IEnumerator SpiralRoutine(float duration, float projectileSpeed, float delay)
    {
        float angle = 0f;
        for (float i = 0; i < duration; i += delay)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bullet = ObjectPool.SpawnObject(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulDir * projectileSpeed;

            angle += 10f;
            yield return new WaitForSeconds(delay);
        }
    }

    public void DoubleSpiral(float projectileSpeed, float duration)
    {
        StartCoroutine(DoubleSpiralRoutine(duration, projectileSpeed, 0.1f));
    }

    public void DoubleSpiral(float projectileSpeed, float duration, float delay)
    {
        StartCoroutine(DoubleSpiralRoutine(duration, projectileSpeed, delay));
    }

    private IEnumerator DoubleSpiralRoutine(float duration, float projectileSpeed, float delay)
    {
        float angle = 0f;

        for (float i = 0; i < duration; i += delay)
        {
            Debug.Log(i);
            for (float j = 0; j < 2; j++)
            {
                float bulDirX = transform.position.x + Mathf.Sin((angle + 180f * j) * Mathf.PI / 180f);
                float bulDirY = transform.position.y + Mathf.Cos((angle + 180f * j) * Mathf.PI / 180f);

                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector2 bulDir = (bulMoveVector - transform.position).normalized;

                GameObject bullet = ObjectPool.SpawnObject(projectile, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = bulDir * projectileSpeed;

                angle += 10f;
                if (angle >= 360f)
                {
                    angle = 0f;
                }
            }
            yield return new WaitForSeconds(delay);
        }   
    }
}

public enum WaveTypes
{
    Circle,
    Half,
    Quarter
}


