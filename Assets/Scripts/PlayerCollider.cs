using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid"))
        {
            gameManager.HandlePlayerHit(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            gameManager.ApplyPowerUp();
            Destroy(collision.gameObject);
            gameManager.powerUps.Remove(collision.gameObject);
        }
    }
}

