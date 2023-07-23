using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text coinText, keyText;
    private int currentCoins = 100, currentKeys = 2;
    const string COIN_AMOUNT_TEXT = "CoinCount", KEY_AMOUNT_TEXT = "KeyCount";

    [SerializeField]
    private AudioClip[] coinPickupClip;

    [SerializeField]
    private AudioClip keyPickupClip;

    private void Start()
    {
        if (keyText == null)
        {
            keyText = GameObject.Find(KEY_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        keyText.text = currentKeys.ToString();

        if (coinText == null)
        {
            coinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        coinText.text = currentCoins.ToString();
    }

    public void ResetInDungeonEconomy()
    {
        currentCoins = 0;
        currentKeys = 2;
    }

    public void UpdateCoins()
    {
        currentCoins += 1;
        SoundFXManager.Instance.PlayRandomSoundFXClip(coinPickupClip, transform, 1f);

        if (coinText == null)
        {
            coinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        coinText.text = currentCoins.ToString();
    }

    public void AddCoins(int num)
    {
        currentCoins += num;

        if (coinText == null)
        {
            coinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        coinText.text = currentCoins.ToString();
    }

    public bool UseCoins(int cost)
    {
        if (currentCoins < cost)
        {
            return false;
        }

        currentCoins -= cost;
        if (coinText == null)
        {
            coinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        coinText.text = currentCoins.ToString();
        return true;
    }

    public void UpdateKeys()
    {
        currentKeys += 1;
        SoundFXManager.Instance.PlaySoundFXClip(keyPickupClip, transform, 1f);

        if (keyText == null)
        {
            keyText = GameObject.Find(KEY_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        keyText.text = currentKeys.ToString();
    }

    public bool UseKey()
    {
        if (currentKeys <= 0)
        {
            return false;
        }

        currentKeys -= 1;
        if (keyText == null)
        {
            keyText = GameObject.Find(KEY_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        keyText.text = currentKeys.ToString();
        return true;
    }
}
