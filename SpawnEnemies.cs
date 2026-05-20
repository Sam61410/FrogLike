using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    public float spawnTimer = 1f;
    public float spawnTimeOut = 4f;

    [SerializeField] Slider healthBar;
    [SerializeField] GameManager gameManager;
    public GameObject[] enemyPrefabs;

    public int enemyType;
    // Update is called once per frame

    public void Start()
    {
        healthBar = FindFirstObjectByType<Slider>();
        gameManager = FindFirstObjectByType<GameManager>(); 
    }
    void Update()
    {
        if (gameManager.canSpawn)
        {
            if (spawnTimer > 0f)
            {
                spawnTimer -= Time.deltaTime;
            }
            else
            {
                SpawnEnemy();
                spawnTimer = UnityEngine.Random.Range(4f,8f);
            }
        }
        else return;
    }
    public void SpawnEnemy()
    {
        Instantiate(enemyPrefabs [enemyType], transform.position, Quaternion.identity);
        enemyType = UnityEngine.Random.Range(0, enemyPrefabs.Length);
        gameManager.UpdateEnemyCount(1);
        spawnTimer = spawnTimeOut;
    }
}
