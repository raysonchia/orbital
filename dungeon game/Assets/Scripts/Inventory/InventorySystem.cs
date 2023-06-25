using Inventory.Model;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InventorySystem : Singleton<InventorySystem>
{
    [SerializeField]
    public InventoryScriptableObject inventoryData;
    public string equipped;

    private void Start()
    {
        equipped = "Azure Sparker";
    }

    public void PickUpWeapon(GameObject weapon)
    {
        WeaponPickUp pickedUp = weapon.GetComponent<WeaponPickUp>();
        if (weapon != null)
        {
            inventoryData.AddItem(pickedUp.weaponSO);
            equipped = pickedUp.weaponSO.Name;
        }
    }

    public int GetInventorySize()
    {
        return inventoryData.GetCurrentInventoryState().Count;
    }

    public string GetNextWeapon()
    {
        Dictionary<int, InventoryItemObject> inventoryState =
            inventoryData.GetCurrentInventoryState();

        int equippedKey =
            inventoryState
            .FirstOrDefault(x => x.Value.weapon.Name == equipped)
            .Key;

        // at last weapon, go to first
        if (equippedKey >= GetInventorySize() - 1)
        {
            equipped = inventoryData.GetItemAt(0).weapon.Name;
        } else
        {
            equipped = inventoryData.GetItemAt(equippedKey + 1).weapon.Name;
        }
        return equipped;
    }

    public string GetPreviousWeapon()
    {
        Dictionary<int, InventoryItemObject> inventoryState =
            inventoryData.GetCurrentInventoryState();

        int equippedKey =
            inventoryState
            .FirstOrDefault(x => x.Value.weapon.Name == equipped)
            .Key;

        // at first equip go to last
        if (equippedKey <= 0)
        {
            equipped = inventoryData.GetItemAt(GetInventorySize() - 1).weapon.Name;
        }
        else
        {
            equipped = inventoryData.GetItemAt(equippedKey - 1).weapon.Name;
        }
        return equipped;
    }
}
