using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    // R�f�rences au GameManager pour acc�der aux donn�es
    private GameManager gameManager;

    // Variables dupliqu�es qui cr�ent des d�pendances
    public float speed;
    public int lives;

    void Start()
    {
        // Recherche du GameManager dans la sc�ne
        gameManager = FindFirstObjectByType<GameManager>();

        // Initialisation des variables
        speed = gameManager.playerSpeed;
        lives = gameManager.lives;
    }

    void Update()
    {
        // Mise � jour des variables depuis le GameManager
        speed = gameManager.playerSpeed;
        lives = gameManager.lives;
    }
}