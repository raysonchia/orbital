using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour
{
    private UIShop uiShop;

    private void Awake()
    {
        uiShop = GameObject.FindWithTag("UIShop").GetComponent<UIShop>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            uiShop.Show();
            player.isFireable = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            uiShop.Hide();
            player.isFireable = true;
        }
    }
}