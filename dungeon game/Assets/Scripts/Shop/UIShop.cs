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
    private Transform[] currentShopTemplates = new Transform[4];
    [SerializeField]
    private AudioClip buyWeaponClip;

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

        if (items.Count >= 4)
        {
            for (int i = 0; i < 4; i++)
            {
                if (items[i] != null)
                {
                    CreateItemButton(items[i], i);
                }
            }
        } else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null)
                {
                    CreateItemButton(items[i], i);
                }
            }
        }

        Hide();
    }

    private void CreateItemButton(ShopScriptableObject shopItem, int positionIndex)
    {
        Sprite itemSprite = shopItem.ItemPrefab.GetComponent<SpriteRenderer>().sprite;
        string itemName = shopItem.ItemPrefab.name;
        int itemCost = Random.Range(shopItem.MinCost, shopItem.MaxCost + 1);

        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        currentShopTemplates[positionIndex] = shopItemTransform;
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 200f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemRectTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemRectTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName.ToString());

        shopItemRectTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() => TryBuyItem(shopItem, itemCost, positionIndex));
    }

    private void TryBuyItem(ShopScriptableObject shopItem, int cost, int positionIndex)
    {
        if (EconomyManager.Instance.UseCoins(cost))
        {
            // weapon
            if (shopItem.IsWeapon)
            {
                if (WeaponPool.Instance.IsInPool(shopItem))
                {
                    ObjectPool.SpawnObject(
                    shopItem.ItemPrefab,
                    GameObject.Find("Shopkeeper").transform.position,
                    Quaternion.identity);
                    WeaponPool.Instance.RemoveFromPool(shopItem);
                    SoundFXManager.Instance.PlaySoundFXClip(buyWeaponClip, transform, 1f);
                }
                else  // im lazy to restructure this ill just refund
                {
                    EconomyManager.Instance.AddCoins(cost);
                    Tooltip.Instance.ShowToolTip("You already have this item!");
                }
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
        else
        {
            Tooltip.Instance.ShowToolTip("Insufficient coins!");
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