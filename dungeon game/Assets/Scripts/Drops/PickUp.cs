using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private enum PickUpType
    {
        Coin,
        Health
    }

    [SerializeField]
    private PickUpType pickUpType;
    [SerializeField]
    private float pickUpDistance = 5f;
    [SerializeField]
    private float accelerationRate = .2f;
    [SerializeField]
    private AnimationCurve animCurve;
    [SerializeField]
    private float heightY = 1.5f;
    [SerializeField]
    private float popDuration = 1f;

    private float moveSpeed = 3f;
    private Vector3 moveDir;
    private Transform player;
    private Rigidbody2D rb;
    private PlayerHealth health; // prob will change health to singleton

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        rb = GetComponent<Rigidbody2D>();
        health = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
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

    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while(timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }

    private void DetectPickupType()
    {
        switch (pickUpType)
        {
            case PickUpType.Coin:
                break;
            case PickUpType.Health:
                health.Heal();
                break;
        }
    }
}
