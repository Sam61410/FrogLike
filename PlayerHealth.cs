using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEditor.PackageManager;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public float damageTimeout;
    [SerializeField] ThirdPersonController player;

    [SerializeField] public LayerMask EnemyLayers;

    public int boxWidth;
    public int boxHeight;
    public int boxDepth;

    [SerializeField] public Slider playerHealthBar;

    public bool enemyFound = false;

    private void Start()
    {
        currentHealth = maxHealth;

        player = FindFirstObjectByType<ThirdPersonController>();

        playerHealthBar = GetComponentInChildren<Slider>();
        playerHealthBar.maxValue = maxHealth;
        playerHealthBar.value = maxHealth;
    }

    public void Update()
    {
        EnemyCheck();
        if (enemyFound && damageTimeout <0)
        {
            TakeDamage(5);
            damageTimeout = 1f;
        }
        else        
        {
            damageTimeout -= Time.deltaTime;
        }
        playerHealthBar.value = currentHealth;
    }

    private void EnemyCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 enemyRadius = new Vector3(boxDepth, boxHeight, boxWidth);
        enemyFound = Physics.CheckBox(spherePosition, enemyRadius, Quaternion.identity, EnemyLayers);
    }

    public void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (enemyFound) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 enemyRadius = new Vector3(boxDepth, boxHeight, boxWidth);
        Gizmos.DrawCube(
        new Vector3(transform.position.x, transform.position.y, transform.position.z), enemyRadius);
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerHealthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }

    }
    public void Die()
    {
        this.gameObject.SetActive(false);
    }
}
