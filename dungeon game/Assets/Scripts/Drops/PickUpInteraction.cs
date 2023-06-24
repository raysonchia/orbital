using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PickUpInteraction : MonoBehaviour
{
    protected Animator animator;
    [SerializeField]
    protected float interactionRange = 3f;
    [SerializeField]
    protected InputActionReference interact;
    protected Transform player;
    protected bool interacted = false;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    protected bool InRange()
    {
        return Vector3.Distance(player.position, transform.position) <= interactionRange
            && interact.action.ReadValue<float>() > 0f
            && interacted == false;
    }
}
