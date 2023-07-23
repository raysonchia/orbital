using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPool : Singleton<ShopPool>
{
    [SerializeField]
    private List<ShopScriptableObject> shopPool;
    public List<ShopScriptableObject> completeShopPool;

    protected override void Awake()
    {
        base.Awake();
        completeShopPool = new List<ShopScriptableObject>(shopPool);
    }


    public List<ShopScriptableObject> GetShopScriptableObjects()
    {
        List<ShopScriptableObject> temp = new List<ShopScriptableObject>(this.shopPool);
        List<ShopScriptableObject> itemsToAdd = new List<ShopScriptableObject>();

        int count = temp.Count;

        if (count >= 4)
        {
            for (int i = 0; i < 4; i++)
            {
                int rand = Random.Range(0, temp.Count);
                itemsToAdd.Add(temp[rand]);
                temp.RemoveAt(rand);
            }
        }
        else if (count < 4)
        {
            for (int i = 0; i < count; i++)
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

    //public void RemoveFromPool(DropsScriptableObject dropToRemove)
    //{
    //    foreach (ShopScriptableObject shopItem in shopPool)
    //    {
    //        if (GameObject.ReferenceEquals(shopItem.ItemPrefab, dropToRemove.DropPrefab))
    //        {
    //            shopPool.Remove(shopItem);
    //            break;
    //        }
    //    }
    //}

    //public void Reset()
    //{
    //    shopPool = new List<ShopScriptableObject>(completeShopPool);
    //}
}