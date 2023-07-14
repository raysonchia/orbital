using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPoison : MonoBehaviour
{
    private PlayerHealth health;
    private Animator animator;

    // to enable previously disabled components if from object pool
    // or just start fade coroutine
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isComplete", false);
        GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine(FadeAnimationRoutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (health = collision.gameObject.GetComponent<PlayerHealth>())
            {
                health.GetHit(transform.gameObject, true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (health = collision.gameObject.GetComponent<PlayerHealth>())
            {
                health.GetHit(transform.gameObject, true);
            }
        }

    }

    public IEnumerator FadeAnimationRoutine()
    {
        yield return StartCoroutine(Wait(3f));
        yield return StartCoroutine(DisableRoutine());
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private IEnumerator DisableRoutine()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        animator.SetBool("isComplete", true);
        yield return null;
    }

    // at last frame of animation
    public void FadeEndAnimationEvent()
    {
        ObjectPool.ReturnObjectToPool(this.gameObject);
    }
}