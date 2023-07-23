using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : Singleton<InventoryController>
    {
        [SerializeField]
        private InventoryPage inventoryUI;

        [SerializeField]
        private InventoryScriptableObject inventoryData;

        public List<InventoryItemObject> initalItems = new List<InventoryItemObject>();

        private void Start()
        {
            ResetInventory();
        }

        public void ResetInventory()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        public void PrepareInventoryData()
        {
            inventoryData.Initialise();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItemObject item in initalItems)
            {
                if (item.IsEmpty())
                {
                    continue;
                }
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItemObject> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.weapon.ItemImage);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.IntialiseInventoryUI(inventoryData.Size);

            this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            this.inventoryUI.OnSwapItems += HandleSwapItems;
            this.inventoryUI.OnStartDragging += HandleDragging;
            this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {

        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItemObject inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty())
            {
                return;
            }
            inventoryUI.CreateDraggedItem(inventoryItem.weapon.ItemImage);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItemObject inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty())
            {
                inventoryUI.ResetSelection();
                return;
            }
            WeaponScriptableObject weapon = inventoryItem.weapon;
            inventoryUI.UpdateDescription(itemIndex, weapon.ItemImage,
                weapon.Name, weapon.Description);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
            {
                inventoryUI = GameObject.Find("InGameCanvas").GetComponentInChildren<InventoryPage>(true);
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key,
                            item.Value.weapon.ItemImage);
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            }
        }
    }
}