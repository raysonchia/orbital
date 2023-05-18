using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 0.3f;
    private float characterVelocity = 0.1f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    
    
    void Start()
    {
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }
    
    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }
    
    void Update()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime){
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }
        
        calcuateNewMovementVector();

        //move enemy: 
        transform.position = new Vector3(transform.position.x + (movementPerSecond.x * Time.deltaTime), 
            transform.position.y + (movementPerSecond.y * Time.deltaTime), 0);
    
    }
}
