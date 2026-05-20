using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Button startButton;
    [SerializeField] public Button tutorialButton;
    [SerializeField] public Button restartButton;
    [SerializeField] TMP_Text enemyCountText;
    [SerializeField] TMP_Text waveCountText;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text winText;
    [SerializeField] GameObject leapText;
    [SerializeField] GameObject dashText;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject background;
    [SerializeField] GameObject shopBackground;

    [SerializeField] ThirdPersonController thirdPersonController;
    [SerializeField] WaveManager waveManager;
    [SerializeField] StarterAssetsInputs starterAssets;
    [SerializeField] SpawnEnemies spawnEnemies;
    [SerializeField] Shoot shoot;
    [SerializeField] Dash dash;
    [SerializeField] Leap leap;

    public bool gameStarted = false;
    public bool canSpawn = false;
    public bool gameOver = false;

    public int enemyCount = 0;

    public void Start()
    {
        shopBackground.SetActive(false);
        waveManager.canSpawn = false;
        starterAssets.SetCursorState(starterAssets.cursorLocked);
        Time.timeScale = 1f;
        enemyCount = 0;
        InitiateUI();
        shoot.canShoot = false;
        leap.canLeap = false;
        dash.canDash = false;
        gameStarted = false;
        canSpawn = false;
        starterAssets.cursorLocked = false;
        thirdPersonController.canMove = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Application.Quit();
        }
        if (gameOver)
        {
            EndGame();
        }
        else if (gameStarted)
        {
            UpdateUI();
        }
        if(enemyCount <=0 )
        {
           // WinGame();
        }
        if (enemyCount > 25)
        {
            canSpawn = false;
        }
    }

    public void Tutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void UpdateUI()
    {
        enemyCountText.text = "Enemies Left: " + enemyCount;
        waveCountText.text = "Wave: " + (waveManager.currentWaveIndex + 1) + "/" + waveManager.waves.Count;
    }

    public void UpdateEnemyCount(int amount)
    {
        enemyCount += amount;
    }

    public void StartGame()
    {
        ActivateUI(); 
        waveManager.canSpawn = true;
        waveManager.StartNextWave();
        shoot.canShoot = true;
        gameStarted = true;
        canSpawn = true;
        gameOverText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        thirdPersonController.LockCameraPosition = false;
        starterAssets.cursorLocked = true;
        starterAssets.SetCursorState(starterAssets.cursorLocked);
        thirdPersonController.canMove = true;
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
        gameOverText.gameObject.SetActive(true);
        gameOver = true;
        restartButton.gameObject.SetActive(true);
        tutorialButton.gameObject.SetActive(true);
        starterAssets.cursorLocked = false;
        starterAssets.SetCursorState(starterAssets.cursorLocked);
        thirdPersonController.canMove = false;
        thirdPersonController.LockCameraPosition = true;
        leap.canLeap = false;
        dash.canDash = false;
    }


    public void InitiateUI()
    {
        enemyCountText.gameObject.SetActive(false);
        waveCountText.gameObject.SetActive(false);
        thirdPersonController.LockCameraPosition = true;
        coin.SetActive(false);
        dashText.SetActive(false);
        leapText.SetActive(false);
        restartButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
        tutorialButton.gameObject.SetActive(true);
        background.SetActive(true);
    }
    public void ActivateUI()
    {
        waveCountText.gameObject.SetActive(true);
        enemyCountText.gameObject.SetActive(true);
        leapText.gameObject.SetActive(true);
        dashText.gameObject.SetActive(true);
        titleText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        tutorialButton.gameObject.SetActive(false);
        coin.SetActive(true);
        background.SetActive(false);
    }

    public void WinGame()
    {
        starterAssets.cursorLocked = false;
        starterAssets.SetCursorState(starterAssets.cursorLocked);
        DeactivateUI();
        restartButton.gameObject.SetActive(true);
        thirdPersonController.canMove = false;
        winText.gameObject.SetActive(true);
    }

    public void DeactivateUI()
    {
        leapText.SetActive(false);
        dashText.SetActive(false);
        enemyCountText.gameObject.SetActive(false);
        waveCountText.gameObject.SetActive(false);
        titleText.gameObject.SetActive(true);
        coin.SetActive(false);
        background.SetActive(true);
    }

    public void ActivateShop()
    {
        enemyCountText.gameObject.SetActive(false);
        dashText.SetActive(false);  
        leapText.SetActive(false);
    }
     public void DeactivateShop()
    {
        enemyCountText.gameObject.SetActive(true);
        dashText.SetActive(true);
        leapText.SetActive(true);   
    }
}
