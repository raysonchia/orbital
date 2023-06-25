using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : PickUpInteraction
{
    // Update is called once per frame
    void Update()
    {
        if (InRange())
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        if (EconomyManager.Instance.UseKey())
        {
            animator.SetTrigger("Open");
            interacted = true;
            if (GetComponent<DropsSpawner>() != null)
            {
                GetComponent<DropsSpawner>().ChestDrops();
            }
        }
    }
}
