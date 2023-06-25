using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "InventoryScriptableObject", menuName = "ScriptableObjects/Inventory")]
    public class InventoryScriptableObject : ScriptableObject
    {
        [SerializeField]
        public List<InventoryItemObject> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 24;

        public event Action<Dictionary<int, InventoryItemObject>> OnInventoryUpdated;

        public void Initialise()
        {
            Debug.Log("setting up");
            inventoryItems = new List<InventoryItemObject>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItemObject.GetEmptyItem());
            }
        }

        public void AddItem(WeaponScriptableObject weapon)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                while (IsInventoryFull() == false)
                {
                    AddItemToFirstFreeSlot(weapon);
                    return;
                }
                InformAboutChange();
            }
        }

        private void AddItemToFirstFreeSlot (WeaponScriptableObject weapon)
        {
            InventoryItemObject newWeapon = new InventoryItemObject
            {
                weapon = weapon
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty())
                {
                    inventoryItems[i] = newWeapon;
                    return;
                }
            }
            return;
        }

        private bool IsInventoryFull()
            => inventoryItems.Where(item => item.IsEmpty()).Any() == false;

        public void AddItem(InventoryItemObject item)
        {
            AddItem(item.weapon);
        }

        public InventoryItemObject GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public Dictionary<int, InventoryItemObject> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItemObject> returnValue =
                new Dictionary<int, InventoryItemObject>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty())
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            if (inventoryItems[itemIndex_1].weapon == null
                || inventoryItems[itemIndex_2].weapon == null)
            {
                return;
            }
            InventoryItemObject item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]
    public struct InventoryItemObject
    {
        public WeaponScriptableObject weapon;
        public bool IsEmpty()
        {
            return weapon == null;
        }

        public static InventoryItemObject GetEmptyItem()
            => new InventoryItemObject
            {
                weapon = null
            };

    }
}