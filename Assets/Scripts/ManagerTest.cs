using UnityEngine;

public class ManagerTest : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject normalShipPrefab;
    [SerializeField] private GameObject asteroidPrefab;

    [SerializeField] private float nextSpawnTime;
    [SerializeField] private float initialSpwanRate;
    [SerializeField] private float spawnRate = 2.0f;

    [SerializeField] private float difficultyInterval = 30f;
    [SerializeField] private float spawnRateDecrease = 0.2f;
    [SerializeField] private float minSpawnRate = 0.5f;

    private bool isGameOver = false;
    [SerializeField] private GameObject gameOverPanel;

    public static System.Action<float> OnTimeChanged;
    private float gameTime;
    private float nextDifficultyTime;

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
        // On instantie le joueur au démarrage, en lui mettant quelconque position de spawn, puisque sa propre méthode
        // Start le replacera où il faut, idem pour les astéroides et les ennemis dans SpawnEnemiesAndAsteroids
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        
        nextSpawnTime = Time.time + spawnRate;
        nextDifficultyTime = Time.time + difficultyInterval;

        // On s'assure qu'au démarrage du jeu, isGameOver est bien false et que le panel est bien désactivé
        gameOverPanel.SetActive(false);
        isGameOver = false;
    }

    void Update()
    {
        if (!isGameOver)
        {
            SpawnEnemiesAndAsteroids();

            gameTime += Time.deltaTime;
            OnTimeChanged?.Invoke(gameTime);

            if (Time.time >= nextDifficultyTime)
            {
                spawnRate -= spawnRateDecrease;

                if (spawnRate < minSpawnRate)
                    spawnRate = minSpawnRate;

                nextDifficultyTime = Time.time + difficultyInterval;
            }
        }
        else
        {
            RestartGame();
        }
    }
    
    void SpawnEnemiesAndAsteroids()
    {
        if (Time.time > nextSpawnTime)
        {
            if (Pcg32.NextFloat() < 0.3f)
            {
                Instantiate(normalShipPrefab, Vector3.zero, Quaternion.identity);
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
