using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEditor.PackageManager;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] ThirdPersonController player;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] CoinManager coinManager;
    [SerializeField] public Slider enemyHealthBar;

    public int damage = 2;
    private int health = 20;
    private int maxHealth = 20;

    public void Start()
    {
        enemyHealthBar = GetComponentInChildren<Slider>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        coinManager = FindFirstObjectByType<CoinManager>();
        enemyHealthBar.maxValue = maxHealth;
        enemyHealthBar.value = maxHealth;
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
        playerHealth.Heal(10);
        coinManager.AddCoins(10);
        Destroy(this.gameObject);
    }
}
