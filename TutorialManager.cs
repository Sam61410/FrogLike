using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject leapText;
    [SerializeField] GameObject dashText;
    [SerializeField] GameObject crosshair;
    [SerializeField] StarterAssetsInputs starterAssets;

    [SerializeField] SpawnEnemies spawnEnemies;
    [SerializeField] Shoot shoot;
    [SerializeField] Dash dash;
    [SerializeField] Leap leap;

    public bool gameStarted = false;

    public int enemyCount = 0;
    public int doorCount = 0;

    public void Start()
    {
        starterAssets.cursorLocked = true;
        starterAssets.SetCursorState(starterAssets.cursorLocked);
        shoot.canShoot = true;
        dash.canDash = true;
        leap.canLeap = true;
        enemyCount = 2;
        doorCount = GameManager.FindObjectsOfType<SpawnEnemies>().Length;
        InitiateUI();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Application.Quit();
        }
    }


    public void UpdateEnemyCount(int amount)
    {
        enemyCount += amount;
    }

    public void UpdateDoorCount(int amount)
    {
        doorCount += amount;
    }

    public void StartGame()
    {
        ActivateUI();
        gameStarted = true;
        shoot.canShoot = true;
        leap.canLeap = true;
        dash.canDash = true;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        DeactivateUI();
        shoot.canShoot = false;
        leap.canLeap = false;
        dash.canDash = false;
    }

    public void InitiateUI()
    {
        dashText.SetActive(false);
        leapText.SetActive(false);
    }
    public void ActivateUI()
    {
        leapText.gameObject.SetActive(true);
        dashText.gameObject.SetActive(true);
        crosshair.gameObject.SetActive(true);
    }

    public void DeactivateUI()
    {
        crosshair.gameObject.SetActive(false);
        leapText.SetActive(false);
        dashText.SetActive(false);
    }
}

