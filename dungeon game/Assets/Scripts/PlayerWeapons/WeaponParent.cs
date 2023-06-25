using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }
    private Vector2 direction;
    [SerializeField]
    private InputActionReference mouseScroll;
    private PlayerMovement player;
    private Image activeWeapon;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        activeWeapon = GameObject.Find("WeaponImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Flip();
        SetSortingLayer();
        CheckSwitchWeapon();
    }

    private void Rotate()
    {
        direction =
            (PointerPosition - (Vector2)transform.position).normalized;

        //rotate weapon
        transform.right = direction;
    }

    private void Flip()
    {
        Vector2 scale = transform.localScale;

        // flip weapon to look upwards when it goes left/right of player
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }

        transform.localScale = scale;
    }

    private void SetSortingLayer()
    {
        // set sorting order of weapon to behind player when it above,
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }

    private void CheckSwitchWeapon()
    {
        if (mouseScroll.action.ReadValue<float>() >= 1f
            || Input.GetKeyDown(KeyCode.Alpha1))
        {
            // prev weapon
            CheckWeaponSwitchable();
            Debug.Log("scroll up");
            DisableCurrentWeapon();

            string nextWeapon = InventorySystem.Instance.GetPreviousWeapon();
            GameObject weaponToActive = FindInActiveObjectByName(nextWeapon);
            SwitchWeapon(weaponToActive);
        }
        else if
            (mouseScroll.action.ReadValue<float>() <= -1f
            || Input.GetKeyDown(KeyCode.Alpha2))
        {
            CheckWeaponSwitchable();
            Debug.Log("scroll down");
            DisableCurrentWeapon();

            string nextWeapon = InventorySystem.Instance.GetNextWeapon();
            GameObject weaponToActive = FindInActiveObjectByName(nextWeapon);
            SwitchWeapon(weaponToActive);
        }
    }

    private void SwitchWeapon(GameObject weaponToActive)
    {
        weaponToActive.SetActive(true);
        player.shoot = weaponToActive.GetComponentInChildren<Shoot>();
        activeWeapon.sprite = weaponToActive.GetComponent<SpriteRenderer>().sprite;
        activeWeapon.SetNativeSize();
    }

    private void DisableCurrentWeapon()
    {
        List<GameObject> currentWeapons = GetAllChildren(gameObject);

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


    private GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    private void CheckWeaponSwitchable()
    {
        if (InventorySystem.Instance.GetInventorySize() <= 1)
        {
            return;
        }
    }
}