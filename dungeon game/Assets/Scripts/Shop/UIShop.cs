using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIShop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;

    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        ShopScriptableObject item = ShopPool.Instance.GetShopScriptableObject();
        GameObject prefab = item.DropPrefab;
        CreateItemButton(prefab.GetComponent<SpriteRenderer>().sprite, prefab.name, item.MinAmount, 0);
        CreateItemButton(prefab.GetComponent<SpriteRenderer>().sprite, prefab.name, item.MinAmount, 1);
        CreateItemButton(prefab.GetComponent<SpriteRenderer>().sprite, prefab.name, item.MinAmount, 2);
    }

    private void CreateItemButton(Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 200f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemRectTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemRectTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName.ToString());

        shopItemRectTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
    }
}