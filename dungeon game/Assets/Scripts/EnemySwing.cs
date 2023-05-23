using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwing : MonoBehaviour
{
    public Transform circleOrgin;
    public float radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrgin == null ? Vector3.zero : circleOrgin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrgin.position, radius))
        {
            Debug.Log("swing " + collider.name);
            PlayerHealth health;
            if (health = collider.GetComponent<PlayerHealth>())
            {
                health.GetHit(transform.parent.gameObject);
            }
        }
    }
}
