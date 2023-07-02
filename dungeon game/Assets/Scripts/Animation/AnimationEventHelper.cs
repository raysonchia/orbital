using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered, OnAttackPerformed;
    public ParticleSystem dust;

    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }

    public void ReturnToPool()
    {
        ObjectPool.ReturnObjectToPool(this.gameObject);
    }

    public void CreateDustParticles()
    {
        dust.Play();
    }
}
