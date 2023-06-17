using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsSpawner : MonoBehaviour
{
    [SerializeField]
    private List<DropsScriptableObject> dropList = new List<DropsScriptableObject>();

    public List<DropsScriptableObject> GetDropsData()
    {
        int rand = Random.Range(1, 101);
        List<DropsScriptableObject> possibleItems = new List<DropsScriptableObject>();
        foreach (DropsScriptableObject drop in dropList)
        {
            if (rand  <= drop.DropChance)
            {
                possibleItems.Add(drop);
            }
        }

        return possibleItems;
    }

    public void SpawnDrops()
    {
        List<DropsScriptableObject> itemsToSpawn = GetDropsData();

        if (itemsToSpawn.Count > 0)
        {
            foreach (DropsScriptableObject drop in itemsToSpawn)
            {
                int randAmount = Random.Range(1, drop.MaxAmount + 1);
                for (int i = 0; i < randAmount; i++)
                {
                    ObjectPool.SpawnObject(drop.DropPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }

    public void WeaponDrop()
    {

    }

    public void ChestDrops()
    {
        foreach (DropsScriptableObject drop in dropList)
        {
            drop.DropChance = 100;
        }

        List<DropsScriptableObject> itemsToSpawn = GetDropsData();

        if (itemsToSpawn.Count > 0)
        {
            Debug.Log(itemsToSpawn.Count);
            foreach (DropsScriptableObject drop in itemsToSpawn)
            {
                int randAmount = Random.Range(drop.MaxAmount, (drop.MaxAmount) * 3 + 1);
                Debug.Log("amount = " + randAmount);
                for (int i = 0; i < randAmount; i++)
                {
                    ObjectPool.SpawnObject(drop.DropPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
