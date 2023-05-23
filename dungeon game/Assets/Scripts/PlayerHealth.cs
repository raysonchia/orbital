using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    private bool isDead = false;

    public void InitialiseHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public void GetHit(GameObject sender)
    {
        if (isDead)
        {
            return;
        }
        if (sender.layer == gameObject.layer)
        {
            return;
        }

        currentHealth -= 1;

        if(currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        } else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            gameObject.SetActive(false);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InitialiseHealth(6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
