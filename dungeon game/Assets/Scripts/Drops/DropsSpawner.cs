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
                int randAmount = Random.Range(drop.MinAmount, drop.MaxAmount + 1);
                for (int i = 0; i < randAmount; i++)
                {
                    SpawnPrefab(drop.DropPrefab);
                }
            }
        }
    }

    public void WeaponDrop()
    {

    }

    public void ChestDrops()
    {
        DropsScriptableObject weaponToDrop = WeaponPool.Instance.GetRandomWeapon();

        if (weaponToDrop != null)
        {
            SpawnPrefab(weaponToDrop.DropPrefab);
        }
        else  // no more weapons, drop coins
        {
            foreach (DropsScriptableObject drop in dropList)
            {
                drop.DropChance = 100;
            }

            List<DropsScriptableObject> itemsToSpawn = GetDropsData();

            if (itemsToSpawn.Count > 0)
            {
                //Debug.Log(itemsToSpawn.Count);
                foreach (DropsScriptableObject drop in itemsToSpawn)
                {
                    int randAmount = Random.Range(drop.MinAmount, drop.MaxAmount + 1);
                    Debug.Log("amount = " + randAmount);
                    for (int i = 0; i < randAmount; i++)
                    {
                        SpawnPrefab(drop.DropPrefab);
                    }
                }
            }
        }

        //Debug.Log("inven: ");
        //foreach (var kvp in InventorySystem.Instance.inventoryData.GetCurrentInventoryState())
        //{
        //    Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value.weapon.Name}");
        //}
    }

    private void SpawnPrefab(GameObject drop)
    {
        GameObject droppedObj = ObjectPool.SpawnObject(drop, transform.position, Quaternion.identity);
        droppedObj.layer = LayerMask.NameToLayer("SpawnedDrops");
        StartCoroutine(CollisionDelayRoutine(droppedObj));
    }

    private IEnumerator CollisionDelayRoutine(GameObject drop)
    {
        yield return new WaitForSeconds(0.5f);
        drop.layer = LayerMask.NameToLayer("Drops");
    }
}
