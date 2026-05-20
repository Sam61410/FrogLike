using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class DoorHealth : MonoBehaviour
{
    [SerializeField] ThirdPersonController player;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] CoinManager coinManager;
    [SerializeField] public Slider enemyHealthBar;

    public int damage = 2;
    private int health = 20;
    public int maxHealth = 20;

    public int coinAmount;
    public int healAmount;

    public void Start()
    {
        health = maxHealth;
        enemyHealthBar = GetComponentInChildren<Slider>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        coinManager = FindFirstObjectByType<CoinManager>();
        enemyHealthBar.maxValue = maxHealth;
        enemyHealthBar.value = maxHealth;
        player = FindFirstObjectByType<ThirdPersonController>();
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
        else
        {
            enemyHealthBar.transform.LookAt(player.transform);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        enemyHealthBar.value = health;
    }

    public void Die()
    {
        playerHealth.Heal(healAmount);
        coinManager.AddCoins(coinAmount);
        Destroy(this.gameObject);
    }
}