using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 pointerInput, movementInput, rotateInput;
    private WeaponParent weaponParent;
    private Shoot shoot;
    Animate animate;

    [SerializeField]
    private InputActionReference movement, mouseDown, pointerPosition;

    [SerializeField]
    private float speed = 3f;

    private float collideDelay = 1.2f;
    private float collisionOffset = 0.05f;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    private bool canMove = true;

    private void OnEnable()
    {
        mouseDown.action.performed += PerformAttack;
    }

    private void OnDisable()
    {
        mouseDown.action.performed -= PerformAttack;
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() == 1f)
        {
            InvokeRepeating("holdShoot", 0f, 0.01f);
        }
        else
        {
            CancelInvoke("holdShoot");
        }
    }

    private void holdShoot()
    {
        shoot.shootAction();
    }

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Scene number:" + SceneManager.GetActiveScene().buildIndex);
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponentInParent<Animate>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        shoot = GetComponentInChildren<Shoot>();
    }

    // Update is called once per frames
    void Update()
    {
        pointerInput = GetPointerInput();
        rotateInput = GetPointerInputRotate();
        weaponParent.PointerPosition = rotateInput;

        if (PlayerHealth.currentHealth <= 0)
        {
            movementInput = Vector3.zero;
            weaponParent.PointerPosition = transform.position;
            pointerInput = Vector3.zero;
            CancelInvoke("holdShoot");
            gameObject.GetComponent<PlayerMovement>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void FixedUpdate()
    {
        movementInput = movement.action.ReadValue<Vector2>();
        Move(movementInput);

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

        // not moving
        if (movementInput.x == 0 && movementInput.y == 0)
        {
            animate.moving = false; // idle animation
        }
        else
        {
            animate.moving = true; // walking animation
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
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    protected void Move(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        //bool success = TryMove(direction);
        //if (!success) {
        //    success = TryMove(new Vector2(direction.x, 0));
        //}

        //if (!success) {
        //    success = TryMove(new Vector2(0, direction.y));
        //}
    }

    private bool TryMove(Vector2 direction) 
    {
        if (canMove) {
            int count = rb.Cast(
                    direction,
                    movementFilter,
                    castCollisions,
                    speed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0) {
                rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
                return true;
            }
        } 
        return false;
    }
}
