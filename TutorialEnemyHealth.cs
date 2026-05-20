using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class TutorialEnemyHealth : MonoBehaviour
{
    public int damage = 2;
    private int health = 20;
    public int maxHealth = 20;
    
    [SerializeField] ThirdPersonController player;
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] public Slider enemyHealthBar;

    public void Start()
    {
        health = maxHealth;
        enemyHealthBar = GetComponentInChildren<Slider>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();
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
        Destroy(this.gameObject);
    }
}