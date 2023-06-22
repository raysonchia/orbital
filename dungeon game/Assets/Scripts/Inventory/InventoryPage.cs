using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;
    [SerializeField]
    private RectTransform contentPanel;
    private List<InventoryItem> listOfUIItems = new List<InventoryItem>();

    public void IntialiseInventoryUI(int inventorySize)
    {
        for (int i  = 0; i < inventorySize; i++)
        {
            InventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = Vector3.one;
            listOfUIItems.Add(uiItem);
        }
    }

    public void toggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
