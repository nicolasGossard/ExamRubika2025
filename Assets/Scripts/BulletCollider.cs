// Script pour les projectiles
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Utilisons OnCollisionEnter au lieu de OnTriggerEnter
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Balle touche ennemi
            gameManager.HandleBulletEnemyCollision(gameObject, collision.gameObject, 100);

            // Chance de g�n�rer un power-up
            if (Random.value < 0.5f)
            {
                gameManager.SpawnPowerUp(collision.transform.position);
            }
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Balle touche ast�ro�de
            gameManager.HandleBulletEnemyCollision(gameObject, collision.gameObject, 50);
        }
    }
}