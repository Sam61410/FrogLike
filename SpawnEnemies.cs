using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEditor.PackageManager;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    public float spawnTimer = 1f;
    public float spawnTimeOut = 4f;

    [SerializeField] Slider healthBar;

    public bool shouldSpawnEnemies = false;

    [SerializeField] public GameObject enemyPrefab;

    // Update is called once per frame
    void Update()
    {
        if (shouldSpawnEnemies)
        {
            if (spawnTimer > 0f)
            {
                spawnTimer -= Time.deltaTime;
            }
            else
            {
                SpawnEnemy();
                spawnTimer = spawnTimeOut;
            }
        }
        else return;
    }
    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        spawnTimer = spawnTimeOut;
    }
}
