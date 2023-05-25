using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
    [SerializeField]
    private Image[] hearts;
    public Sprite fullHeart, halfHeart, emptyHeart;

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
        UpdateHealthUI();
        Debug.Log(currentHealth + " health left");

        if(currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        } else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            animator.SetTrigger("isDead");
            Debug.Log("dead");
            StartCoroutine(GameOver());
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InitialiseHealth(6);
        UpdateHealthUI();
        animator = GetComponent<Animator>();
    }

    private IEnumerator GameOver()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(2);
        //SceneManager.LoadScene("") implement death scene later
    }

    public void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            int heartStatusRemainder = (int) Mathf.Clamp(currentHealth - (i * 2), 0, 2);
            hearts[i].sprite = SetHeartImage((HeartStatus) heartStatusRemainder);
        }
    }

    private enum HeartStatus
    {
        Empty = 0,
        Half = 1,
        Full = 2
    }

    private Sprite SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                return emptyHeart;
            case HeartStatus.Half:
                return halfHeart;
            case HeartStatus.Full:
                return fullHeart;
            default:
                return emptyHeart;
        }
    }
}
