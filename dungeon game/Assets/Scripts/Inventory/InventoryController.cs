using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : Singleton<InventoryController>
{
    [SerializeField]
    private InventoryPage inventoryUI;
    public int inventorySize = 16;

    private void Start()
    {
        inventoryUI.IntialiseInventoryUI(inventorySize);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.toggleActive();
        }
    }
}
