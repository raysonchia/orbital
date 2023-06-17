using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private float interactionRange = 3f;
    [SerializeField]
    private InputActionReference interact;
    private Transform player;
    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= interactionRange
            && interact.action.ReadValue<float>() > 0f
            && opened == false)
        {
            animator.SetTrigger("Open");
            opened = true;
            if (GetComponent<DropsSpawner>() != null)
            {
                GetComponent<DropsSpawner>().ChestDrops();
            }
        }
    }
}
