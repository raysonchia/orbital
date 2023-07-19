using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPool : Singleton<ShopPool>
{
    [SerializeField]
    private List<ShopScriptableObject> shopPool;

    protected override void Awake()
    {
        base.Awake();
    }

    //public DropsScriptableObject GetRandomWeapon()
    //{
    //    if (weaponPool.Count > 0)
    //    {
    //        int rand = Random.Range(0, weaponPool.Count);
    //        DropsScriptableObject weaponToDrop = this.weaponPool[rand];
    //        this.weaponPool.RemoveAt(rand);
    //        return weaponToDrop;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}
    public List<ShopScriptableObject> GetShopScriptableObjects()
    {
        List<ShopScriptableObject> temp = new List<ShopScriptableObject>(this.shopPool);
        List<ShopScriptableObject> itemsToAdd = new List<ShopScriptableObject>();

        if (temp.Count > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                int rand = Random.Range(0, temp.Count);
                itemsToAdd.Add(temp[rand]);
                temp.RemoveAt(rand);
            }
        }
        else
        {
            return null;
        }

        return itemsToAdd;
    }

    public void Reset()
    {
        
    }
}