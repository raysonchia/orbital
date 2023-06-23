using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }
    private Vector2 direction;
    [SerializeField]
    private InputActionReference mouseScroll;

    // Update is called once per frame
    void Update()
    {

        Rotate();
        Flip();
        SetSortingLayer();
        //CheckSwitchWeapon();
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
        if (mouseScroll.action.ReadValue<float>() > 0.1f)
        {
            // prev weapon
            Debug.Log("scroll up");
        }
        else if (mouseScroll.action.ReadValue<float>() < 0.1f)
        {
            Debug.Log("scroll down");
        }
    }


}