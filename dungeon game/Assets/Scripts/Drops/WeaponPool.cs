using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class WeaponPool : Singleton<WeaponPool>
{
    [SerializeField]
    public List<DropsScriptableObject> weaponPool;
    private List<DropsScriptableObject> completeWeaponPool;

    protected override void Awake()
    {
        base.Awake();
        this.completeWeaponPool = new List<DropsScriptableObject> (this.weaponPool);
    }

    public DropsScriptableObject GetRandomWeapon()
    {
        if (weaponPool.Count > 0)
        {
            int rand = Random.Range(0, weaponPool.Count);
            DropsScriptableObject weaponToDrop = this.weaponPool[rand];
            this.weaponPool.RemoveAt(rand);
            return weaponToDrop;
        }
        else
        {
            return null;
        }
    }

    public void Reset()
    {
        
    }
}