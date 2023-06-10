using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    public static GameObject SpawnObject(GameObject @objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        // if the pool doesn't exist, create it
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        // check for inactive objects in the pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        //GameObject spawnableObj = null;
        //foreach (GameObject obj in pool.InactiveObjects)
        //{
        //    if (obj != null)
        //    {
        //        spawnableObj = obj;
        //        Debug.Log(spawnableObj + "");
        //        break;
        //    }
        //}


        if (spawnableObj == null)
        {
            
            // if there are no inactive objects, create one
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        } else
        {
            // if there is an inactive object, reactive it
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            spawnableObj.GetComponent<Bullet>().startPosition = spawnPosition;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7); // remove "Clone" at end of string
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if (pool == null)
        {
            Debug.LogWarning("Trying to relase an object that is not pooled " + obj.name);
        } else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
}



public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}