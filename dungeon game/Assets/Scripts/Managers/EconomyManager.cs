using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text coinText, keyText;
    private int currentCoins = 0, currentKeys = 0;
    const string COIN_AMOUNT_TEXT = "CoinCount", KEY_AMOUNT_TEXT = "KeyCount";

    public void ResetInDungeonEconomy()
    {
        currentCoins = 0;
        currentKeys = 0;
    }

    public void UpdateCoins()
    {
        currentCoins += 1;
        if (coinText == null)
        {
            coinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        coinText.text = currentCoins.ToString();
    }

    public void UpdateKeys()
    {
        currentKeys += 1;
        if (keyText == null)
        {
            keyText = GameObject.Find(KEY_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        keyText.text = currentKeys.ToString();
    }
}
