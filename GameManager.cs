using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEditor.PackageManager;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Button startButton;
    [SerializeField] ThirdPersonController thirdPersonController;
    [SerializeField] StarterAssetsInputs starterAssets;
    [SerializeField] SpawnEnemies spawnEnemies;
    [SerializeField] Shoot shoot;
    [SerializeField] Dash dash;
    [SerializeField] Leap leap;

    public void Start()
    {
        thirdPersonController.LockCameraPosition = true;
        starterAssets.cursorLocked = false;
        spawnEnemies.shouldSpawnEnemies = false;
        thirdPersonController.canMove = false;
        shoot.canShoot = false;
        leap.canLeap = false;
        dash.canDash = false;
    }
    public void StartGame()
    {
        thirdPersonController.LockCameraPosition = false;
        startButton.gameObject.SetActive(false);
        spawnEnemies.shouldSpawnEnemies = true;
        starterAssets.cursorLocked = true;
        starterAssets.SetCursorState(starterAssets.cursorLocked);
        thirdPersonController.canMove = true;
        shoot.canShoot = true;
        leap.canLeap = true;
        dash.canDash = true;
    }
}
