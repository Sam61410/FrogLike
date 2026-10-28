using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] TMP_Text coinText;

    public int coinAmount;
    public int startingCoinAmount = 0;
    public int coinMultiplier;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinAmount = startingCoinAmount;
        coinText = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coins: " + coinAmount;
    }

    public void CoinMultiply(int amount)
    {
        coinMultiplier += amount;
    }

    public void AddCoins(int amount)
    {
        coinAmount += (amount * coinMultiplier);
    }

    public int SpendCoins(int amount)
    {
        if (coinAmount >= amount)
        {
            coinAmount -= amount;
            return 0; 
        }
        else
        {
            return -1; 
        }
    }
}
