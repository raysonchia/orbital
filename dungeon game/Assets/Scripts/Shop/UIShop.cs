using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;

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
        InitialiseShop();
    }

    public void InitialiseShop()
    {
        List<ShopScriptableObject> items = ShopPool.Instance.GetShopScriptableObjects();
        for (int i = 0; i < 4; i++)
        {
            CreateItemButton(items[i], i);
        }

        Hide();
    }

    private void CreateItemButton(ShopScriptableObject shopItem, int positionIndex)
    {
        Sprite itemSprite = shopItem.ItemPrefab.GetComponent<SpriteRenderer>().sprite;
        string itemName = shopItem.ItemPrefab.name;
        int itemCost = Random.Range(shopItem.MinCost, shopItem.MaxCost + 1);

        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 200f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemRectTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemRectTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName.ToString());

        shopItemRectTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() => TryBuyItem(shopItem, itemCost));
    }

    private void TryBuyItem(ShopScriptableObject shopItem, int cost)
    {
        if (EconomyManager.Instance.UseCoins(cost))
        {
            // weapon
            if (shopItem.IsWeapon)
            {
                ObjectPool.SpawnObject(
                    shopItem.ItemPrefab,
                    GameObject.Find("Shopkeeper").transform.position,
                    Quaternion.identity);
            }
            else
            {
                // non weapon, keys/health
                PickUpType nonWeaponType = shopItem.ItemPrefab.GetComponent<PickUp>().pickUpType;
                switch (nonWeaponType)
                {
                    case PickUpType.Health:
                        PlayerHealth.Instance.Heal();
                        break;
                    case PickUpType.Key:
                        EconomyManager.Instance.UpdateKeys();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}