using UnityEngine;
[System.Serializable]

public class Wave
{
    public string waveName;
    public GameObject[] enemyPrefabs;
    public int enemyCount = 5;
    public float spawnInterval = 1f;
}

