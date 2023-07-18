using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPool : Singleton<ShopPool>
{
    [SerializeField]
    private ShopScriptableObject shopPool;

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
    public ShopScriptableObject GetShopScriptableObject()
    {
        return shopPool;
    }

    public void Reset()
    {
        
    }
}