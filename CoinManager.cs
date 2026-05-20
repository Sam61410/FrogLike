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

    public int coinAmount = 0;
    public int startingCoinAmount = 0;

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

    public void AddCoins(int amount)
    {
        coinAmount += amount;
    }

    public sbyte SpendCoins(int amount)
    {
        if (coinAmount >= amount)
        {
            coinAmount -= amount;
            return 0; // Success
        }
        else
        {
            return -1; // Not enough coins
        }
    }
}
