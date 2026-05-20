using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;
    public float damageTimeout;
    public float bulletDamageTimeout;
    public int enemyDamage;
    [SerializeField] ThirdPersonController player;
    [SerializeField] GameManager gameManager;

    [SerializeField] public LayerMask EnemyLayers;
    [SerializeField] public LayerMask KillBoxLayers;
    [SerializeField] public LayerMask bulletLayers;

    public int boxWidth;
    public int boxHeight;
    public int boxDepth;

    [SerializeField] public Slider playerHealthBar;

    public bool enemyFound = false;
    public bool KILLBOX = false;
    public bool bullet = false;

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
        BulletCheck();
        if (bullet && bulletDamageTimeout < 0)
        {
            TakeBulletDamage(enemyDamage);
        }
        else
        {
            bulletDamageTimeout -= Time.deltaTime;
        }
        if (enemyFound && damageTimeout <0)
        {
            TakeDamage(enemyDamage);
            damageTimeout = 1f;
        }
        else        
        {
            damageTimeout -= Time.deltaTime;
        }
        playerHealthBar.value = currentHealth;
        if(KILLBOX)
        {
            Die();
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void EnemyCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 enemyRadius = new Vector3(boxDepth,boxHeight + 0.5f, boxWidth);
        enemyFound = Physics.CheckBox(spherePosition, enemyRadius, Quaternion.identity, EnemyLayers);
        KILLBOX = Physics.CheckBox(spherePosition, enemyRadius, Quaternion.identity, KillBoxLayers);
   }

    private void BulletCheck()
    {
        Vector3 bulletSpherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float bulletEnemyRadius = 0.5f;
        bullet = Physics.CheckSphere(bulletSpherePosition, bulletEnemyRadius, bulletLayers);
    }

    public void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (enemyFound) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Vector3 bulletSpherePosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        float bulletEnemyRadius = 0.5f;
        Gizmos.DrawSphere(bulletSpherePosition, bulletEnemyRadius);

        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 enemyRadius = new Vector3(boxDepth, boxHeight +0.5f, boxWidth);
        Gizmos.DrawCube(
        new Vector3(transform.position.x, transform.position.y, transform.position.z), enemyRadius);
    }


    public void Heal(float healAmount)
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
        damageTimeout = 1f;
    }

    public void TakeBulletDamage(int damage)
    {
        currentHealth -= damage;
        playerHealthBar.value = currentHealth;
        bulletDamageTimeout = 1f;
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        gameManager.EndGame();
    }
}