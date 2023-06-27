using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDropSpawner : Singleton<BossDropSpawner>
{
    private Collider2D spawnableAreaCollider;

    [SerializeField]
    private GameObject[] dropsToSpawn;

    [SerializeField]
    private LayerMask layersDropsCannotSpawnOn;

    private void Start()
    {
        spawnableAreaCollider = GetComponent<BoxCollider2D>();
    }

    public void SpawnDrop()
    {
        foreach (GameObject drop in dropsToSpawn)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition(spawnableAreaCollider);
            Instantiate(drop, spawnPosition, Quaternion.identity);
        }
    }

    public Vector2 GetRandomSpawnPosition(Collider2D spawnableAreaCollider)
    {
        Vector2 spawnPosition = Vector2.zero;
        bool isSpawnPosValid = false;

        int attemptCount = 0;
        int maxAttempts = 200;

        while (!isSpawnPosValid && attemptCount < maxAttempts)
        {
            spawnPosition = GetRandomPointInCollider(spawnableAreaCollider);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 2f);

            bool isInvalidCollision = false;
            foreach (Collider2D collider in colliders)
            {
                if (((1 << collider.gameObject.layer) & layersDropsCannotSpawnOn) != 0)
                {
                    isInvalidCollision = true;
                    break;
                }
            }

            if (!isInvalidCollision)
            {
                isSpawnPosValid = true;
            }

            attemptCount++;
        }

        if (!isSpawnPosValid)
        {
            Debug.LogWarning("Could not find a valid spawn position");
        }

        return spawnPosition;
    }

    private Vector2 GetRandomPointInCollider(Collider2D collider, float offset = .5f)
    {
        Bounds collBounds = collider.bounds;

        Vector2 minBounds = new Vector2(collBounds.min.x + offset, collBounds.min.y + offset);
        Vector2 maxBounds = new Vector2(collBounds.max.x - offset, collBounds.max.y - offset);

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomX, randomY);
    }
}
