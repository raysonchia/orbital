using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "InventoryScriptableObject", menuName = "ScriptableObjects/Inventory")]
    public class InventoryScriptableObject : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItemObject> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 24;

        public event Action<Dictionary<int, InventoryItemObject>> OnInventoryUpdated;

        public void Initialise()
        {
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
                if (inventoryItems[i].IsEmpty())
                {
                    inventoryItems[i] = new InventoryItemObject
                    {
                        weapon = weapon
                    };
                    return;
                }
            }
        }

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