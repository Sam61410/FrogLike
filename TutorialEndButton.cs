using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class TutorialEndButton : MonoBehaviour
{
    [SerializeField] TMP_Text doorText;
    [SerializeField] StarterAssetsInputs starterAssets;

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
            SceneManager.LoadScene(0);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        doorText.text = "Are you sure you want to move on?";
    }

    public void Die()
    {
        starterAssets.cursorLocked = false;
        Destroy(this.gameObject);
    }
}
