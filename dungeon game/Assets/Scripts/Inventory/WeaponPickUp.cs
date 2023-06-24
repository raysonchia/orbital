using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class WeaponPickUp : PickUpInteraction
{
    [field: SerializeField]
    public WeaponScriptableObject weapon { get; private set; }
    [SerializeField]
    private GameObject pickUpAnimation;

    private GameObject weaponParent;
    private List<GameObject> currentWeapons;
    private Image activeWeapon;
    private SpriteRenderer sr;

    private void Start()
    {
        weaponParent = GameObject.Find("WeaponParent");
        activeWeapon = GameObject.Find("WeaponImage").GetComponent<Image>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        {
            if (InRange())
            {
                PickUpWeapon();
            }
        }
    }

    private void PickUpWeapon()
    {
        DisableCurrentWeapon();
        ObjectPool.SpawnObject(pickUpAnimation, transform.position, Quaternion.identity);

        // swap attack to picked up weapon
        player.gameObject.GetComponent<PlayerMovement>().shoot =
            GetComponentInChildren<Shoot>();
        weaponParent.GetComponent<WeaponParent>().weaponRenderer = sr;
        activeWeapon.sprite = sr.sprite;
        activeWeapon.SetNativeSize();
        MoveToPlayer();
        this.enabled = false;
    }

    private void MoveToPlayer()
    {
        gameObject.transform.SetParent(weaponParent.transform);
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.transform.localPosition =
            new Vector3(weapon.WeaponPos.x, weapon.WeaponPos.y);
    }

    private void DisableCurrentWeapon()
    {
        currentWeapons = GetAllChildren(weaponParent);

        foreach (GameObject curr in currentWeapons)
        {
            if (curr.activeInHierarchy)
            {
                curr.SetActive(false);
                break;
            }
        }
    }

    private List<GameObject> GetAllChildren(GameObject parent)
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in parent.transform)
        {
            children.Add(child.gameObject);
            children.AddRange(GetAllChildren(child.gameObject));
        }

        return children;
    }
}
