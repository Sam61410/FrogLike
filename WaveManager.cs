using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public List<Wave> waves = new List<Wave>();
    public Transform[] spawnPoints;

    [Header("Events")]
    public UnityEvent<int> OnWaveStarted;  
    public UnityEvent<int> OnWaveCompleted; 
    public UnityEvent OnAllWavesCompleted;

    public int currentWaveIndex = -1;
    private int enemiesAlive = 0;
    private bool spawning = false;
    public bool canSpawn = false;

    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] ShopManager shop;

    void Start()
    {
        canSpawn = false;
        if (waves.Count == 0)
        {
            Debug.LogWarning("No waves configured in WaveManager.");
            return;
        }
    }



    public IEnumerator ShopRoutine()
    {
        if (enemiesAlive == 0 && !spawning && canSpawn)
        {
            OnWaveCompleted?.Invoke(currentWaveIndex);
            StartNextWave();
            Debug.Log("Starting next wave...");
        }
        yield return null;
    }

    public void StartNextWave()
    {
        currentWaveIndex++;
        if (currentWaveIndex >= waves.Count)
        {
            OnAllWavesCompleted?.Invoke();
            Debug.Log("All waves completed!");
            return;
        }
        playerHealth.Heal(10 * ((currentWaveIndex + 1))/2);

        Wave wave = waves[currentWaveIndex];
        OnWaveStarted?.Invoke(currentWaveIndex);
        StartCoroutine(SpawnWave(wave));
    }


    private IEnumerator SpawnWave(Wave wave)
    {
        spawning = true;
        enemiesAlive = wave.enemyCount;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefabs);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        spawning = false;
    }


    private void SpawnEnemy(GameObject[] enemyPrefabs)
    {   
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
        gameManager.enemyCount++;

        EnemyHealth enemyScript = enemy.GetComponent<EnemyHealth>();
        if (enemyScript != null)
        {
            enemyScript.OnDeath += HandleEnemyDeath;
        }
        else
        {
            Debug.LogWarning("Spawned enemy has no EnemyHealth script with OnDeath event.");
        }
    }


    private void HandleEnemyDeath()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0 && !spawning)
        {
            OnWaveCompleted?.Invoke(currentWaveIndex);
            Debug.Log("Wave " + (currentWaveIndex + 1) + " completed!");
            shop.OpenShop();
        }
    }
}