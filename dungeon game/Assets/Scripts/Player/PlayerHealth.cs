using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Inventory;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public static int currentHealth, maxHealth;
    private float invulTime = 1.2f;
    private bool isInvul;
    private SpriteRenderer playerSprite;
    private Material defaultMat;
    [SerializeField]
    public Material flashMaterial;
    private Animator animator;
    public UnityEvent<GameObject> OnHitWithReference, OnDamagedWithReference, OnDeathWithReference;

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
        if (SceneManager.GetActiveScene().name != "Base")
        {
            UpdateHealthUI();
        }

        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        defaultMat = playerSprite.material;
        restartScreen.SetActive(false);
    }

    public void InitialiseHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public void Heal()
    {
        if (!isDead)
        {
            currentHealth += 1;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            } else
            {
                Debug.Log("healing");
                UpdateHealthUI();
            }
        }
    }

    public void GetHit(GameObject sender, bool isBodyCollision = false)
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
            if (isBodyCollision)
            {
                OnHitWithReference?.Invoke(sender);
            }
            StartCoroutine(BecomeInvulnerable());
            OnDamagedWithReference?.Invoke(gameObject);
            //StartCoroutine(Damaged());
        } else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            animator.SetTrigger("isDead");
            EconomyManager.Instance.ResetInDungeonEconomy();
            InventoryController.Instance.ResetInventory();
            Debug.Log("dead");
            CancelInvoke();
            StartCoroutine(GameOver());
        }
        
    }

    private IEnumerator BecomeInvulnerable()
    {
        isInvul = true;
        yield return new WaitForSeconds(invulTime);
        isInvul = false;
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
