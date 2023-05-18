using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 pointerInput, movementInput, rotateInput;
    private WeaponParent weaponParent;
    private Shoot shooting;
    Animate animate;

    [SerializeField]
    private InputActionReference movement, mouseDown, pointerPosition;

    [SerializeField]
    private float speed = 3f;

    private void OnEnable()
    {
        mouseDown.action.started += PerformAttack;
    }

    private void OnDisable()
    {
        mouseDown.action.started -= PerformAttack;
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        shooting.shootAction();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponentInParent<Animate>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        shooting = GetComponentInChildren<Shoot>();
    }

    // Update is called once per frames
    void Update()
    {
        pointerInput = GetPointerInput();
        rotateInput = GetPointerInputRotate();
        weaponParent.PointerPosition = rotateInput;
        movementInput = movement.action.ReadValue<Vector2>();

        // not moving
        if (movementInput.x == 0 && movementInput.y == 0)
        {
            animate.moving = false; // idle animation
        }
        else
        {
            animate.moving = true; // walking animation
        }

        //if (shoot.action.inProgress)
    }

    private void FixedUpdate()
    {
        movementInput *= speed;
        rb.velocity = movementInput;

        // flip the player sprite according to mouse pos
        if (pointerInput.x > 0)
        {
            transform.localScale = Vector3.one;
            // make the weapon not flip with player sprite
            weaponParent.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (pointerInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            // make the weapon not flip with player sprite
            weaponParent.transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    // used to flip player sprite according to mouse position
    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        Vector3 charPos = Camera.main.transform.position;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos) - charPos;
    }

    // used to rotate weapon sprite according to mouse position
    private Vector2 GetPointerInputRotate()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        Vector3 charPos = Camera.main.transform.position;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos - charPos);
    }
}
