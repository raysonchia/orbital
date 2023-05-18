using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction =
            (PointerPosition - (Vector2) transform.position).normalized;

        //rotate weapon
        transform.right = direction;

        Vector2 scale = transform.localScale;

        // flip weapon to look upwards when it goes left/right of player
        if (direction.x < 0)
        {
            scale.y = -1;
        } else if(direction.x > 0)
        {
            scale.y = 1;
        }

        transform.localScale = scale;

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
}