using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class ManagerTest : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject asteroidPrefab;

    [SerializeField] private float nextSpawnTime;
    [SerializeField] private float initialSpwanRate;
    [SerializeField] private float spawnRate = 2.0f;

    private bool isGameOver = false;
    [SerializeField] private GameObject gameOverPanel;

    private void OnEnable()
    {
        Player.OnPlayerDied += GameOver;
    }

    private void OnDisable()
    {
        Player.OnPlayerDied -= GameOver;
    }

    void Start()
    {
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        nextSpawnTime = Time.time + spawnRate;

        gameOverPanel.SetActive(false);
        isGameOver = false;
    }

    void Update()
    {
        if (!isGameOver)
            SpawnEnemiesAndAsteroids();
        else
            RestartGame();
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

    private void GameOver()
    {
        if (isGameOver)
            return;
        
        isGameOver = true;

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;

            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
