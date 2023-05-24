using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static int currentHealth, maxHealth;
    //private float invulTime = 1.25f;
    //private bool isInvul;
    private Animator animator;
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
        Debug.Log(currentHealth + " health left");

        if(currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        } else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            animator.SetTrigger("isDead") ;
            Debug.Log("dead");
            StartCoroutine(GameOver());
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InitialiseHealth(6);
        animator = GetComponent<Animator>();
    }

    private IEnumerator GameOver()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(2);
        //SceneManager.LoadScene("") implement death scene later
    }
}
