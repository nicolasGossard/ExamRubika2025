using System.Collections;
using UnityEngine;

public class ManagerTest : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject asteroidPrefab;

    [SerializeField] private float nextSpawnTime;
    [SerializeField] private float initialSpwanRate;
    [SerializeField] private float spawnRate = 2.0f;

    private bool isGameOver = false;

    void Start()
    {
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        nextSpawnTime = Time.time + spawnRate;
    }

    void Update()
    {
        SpawnEnemiesAndAsteroids();
    }
    
    void SpawnEnemiesAndAsteroids()
    {
        if (Time.time > nextSpawnTime)
        {
            if (Pcg32.NextFloat() < 0.3f)
            {
                Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Instantiate(asteroidPrefab, Vector3.zero, Quaternion.identity);
            }

            nextSpawnTime = Time.time + spawnRate;
        }
    }
}
