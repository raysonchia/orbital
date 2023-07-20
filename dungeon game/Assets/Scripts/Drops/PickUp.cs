using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpType
{
    Coin,
    Health,
    Key,
    Weapon
}

public class PickUp : MonoBehaviour
{
    [SerializeField]
    public PickUpType pickUpType;
    [SerializeField]
    private float pickUpDistance = 5f;
    [SerializeField]
    private float accelerationRate = .2f;
    [SerializeField]
    private AnimationCurve animCurve;

    private float moveSpeed = 3f;
    private Vector3 moveDir;
    private Transform player;
    private Rigidbody2D rb;
    private PlayerHealth health; // prob will change health to singleton
    private DropAnimation dropAnimator;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        rb = GetComponent<Rigidbody2D>();
        health = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
        dropAnimator = GetComponent<DropAnimation>();
    }

    private void Start()
    {
        StartCoroutine(
            dropAnimator.AnimCurveSpawnRoutine(
                animCurve,
                gameObject.transform));
    }

    private void Update()
    {
        Vector3 playerPos = player.position;

        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelerationRate; // acceleration effect
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            //ObjectPool.ReturnObjectToPool(gameObject); // next time
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    private void DetectPickupType()
    {
        switch (pickUpType)
        {
            case PickUpType.Coin:
                EconomyManager.Instance.UpdateCoins();
                break;
            case PickUpType.Key:
                EconomyManager.Instance.UpdateKeys();
                break;
            case PickUpType.Health:
                health.Heal();
                break;
        }
    }
}