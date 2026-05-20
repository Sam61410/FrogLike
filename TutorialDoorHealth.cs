using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class TutorialDoorHealth : MonoBehaviour
{
    [SerializeField] TMP_Text doorText;

    public int damage = 2;
    private int health = 20;
    public int maxHealth = 20;

    public void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        doorText.text = "Are you sure you want to move on?";
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
