using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static int currentHealth, maxHealth;
    private float invulTime = 1.2f;
    private bool isInvul;
    private SpriteRenderer playerSprite;
    private Material defaultMat;
    [SerializeField]
    public Material flashMaterial;
    private Animator animator;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    private bool isDead = false;
    [SerializeField]
    private Image[] hearts;
    [SerializeField]
    public Sprite fullHeart, halfHeart, emptyHeart;
    public GameObject restartScreen;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseHealth(6);
        UpdateHealthUI();
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        defaultMat = playerSprite.material;
    }

    public void InitialiseHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public void GetHit(GameObject sender)
    {
        if (isDead || isInvul)
        {
            return;
        }

        currentHealth -= 1;
        UpdateHealthUI();
        Debug.Log(currentHealth + " health left");

        if(currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
            StartCoroutine(BecomeInvulnerable());
            StartCoroutine(Damaged());
        } else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            animator.SetTrigger("isDead");
            Debug.Log("dead");
            CancelInvoke();
            StartCoroutine(GameOver());
        }
        
    }

    private IEnumerator Damaged()
    {
        yield return StartCoroutine(Flash());

        for (int i = 0; i < 5; i++)
        {
            yield return StartCoroutine(Wait());
            yield return StartCoroutine(Blink());
        }
    }

    private IEnumerator BecomeInvulnerable()
    {
        isInvul = true;
        yield return new WaitForSeconds(invulTime);
        isInvul = false;
    }

    private IEnumerator Flash()
    {
        playerSprite.material = flashMaterial;
        yield return new WaitForSeconds(0.2f);
        playerSprite.material = defaultMat;
    }

    private IEnumerator Blink()
    {
        playerSprite.enabled = false;
        yield return new WaitForSeconds(0.1f);
        playerSprite.enabled = true;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator GameOver()
    {
        //gameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(2);
        restartScreen.SetActive(true);
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
                Debug.Log("empty");
                return emptyHeart;
            case HeartStatus.Half:
                Debug.Log("half");
                return halfHeart;
            case HeartStatus.Full:
                Debug.Log("full");
                return fullHeart;
            default:
                return emptyHeart;
        }
    }
}
