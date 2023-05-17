using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 pointerInput, movementInput;
    Animate animate;

    [SerializeField]
    private InputActionReference movement, shoot, pointerPosition;

    [SerializeField]
    private float speed = 3f;

    //private void OnEnable()
    //{
    //    shoot.action.performed += PerformAttack;
    //}

    //private void OnDisable()
    //{
    //    shoot.action.performed -= PerformAttack;
    //}

    //private void PerformAttack(InputAction.CallbackContext obj)
    //{
    //    if ()
    //}

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animate>();
    }

    // Update is called once per frames
    void Update()
    {
        pointerInput = GetPointerInput();
        movementInput = movement.action.ReadValue<Vector2>();

        if (movementInput.x == 0 && movementInput.y == 0)
        {
            animate.moving = false;
        }
        else
        {
            animate.moving = true;
        }
        //if (shoot.action.inProgress)
    }

    private void FixedUpdate()
    {
        movementInput *= speed;
        rb.velocity = movementInput;

        if (pointerInput.x > 0)
        {
            transform.localScale = Vector3.one;
        } else if (pointerInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        Vector3 charPos = Camera.main.transform.position;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos) - charPos;
    }
}
