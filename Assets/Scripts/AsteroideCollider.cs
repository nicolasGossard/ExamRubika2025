// Script pour les ast�ro�des
using UnityEngine;

public class AsteroidCollider : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Utilisons OnCollisionEnter au lieu de OnTriggerEnter
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Le joueur a touch� un ast�ro�de
            gameManager.HandlePlayerHit(gameObject);
        }
    }
}