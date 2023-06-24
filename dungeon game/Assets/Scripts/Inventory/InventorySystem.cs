using Inventory.Model;
using UnityEngine;
using System.Collections;

public class InventorySystem : Singleton<InventorySystem>
{
    [SerializeField]
    private InventoryScriptableObject inventoryData;
    

    public void PickUpWeapon(GameObject weapon)
    {
        WeaponPickUp pickedUp = weapon.GetComponent<WeaponPickUp>();
        if (weapon != null)
        {
            inventoryData.AddItem(pickedUp.weaponSO);
        }
    }
}
