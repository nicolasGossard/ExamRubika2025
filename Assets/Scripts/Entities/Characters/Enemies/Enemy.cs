using UnityEngine;
using System.Collections;

public abstract class Enemy : Character
{
    public static event System.Action<int> AddScore;
    public static event System.Action<GameObject> OnEnemyDestroyed;

    [Header("Paramètres de l'ennemi")]

    [SerializeField] private int enemyValue;
    
    [SerializeField] private Renderer rend;
    private Color originalColor;

    protected virtual void Start()
    {
        float randomX = Pcg32.RangeFloat(limitsX.x, limitsX.y);
        transform.position = new Vector3(randomX, 0, limitsZ.x);
        isSpawned = true;

        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }

        originalColor = rend.material.color;
    }
    
    protected override void LimitPosition(Vector3 position)
    {
        if (position.z < limitsZ.y)
        {
            Player player = FindFirstObjectByType<Player>();

            if (player != null)
            {
                player.TakeDammage(1);
            }

            Destroy();
        }
    }

    public override void TakeDammage(int amount)
    {
        livesEntity -= amount;

        if (livesEntity <= 0)
        {
            AddScorePlayer(enemyValue);
            Destroy();
        }
    }

    public override void Destroy()
    {
        OnEnemyDestroyed?.Invoke(gameObject);

        isSpawned = false;
        enabled = false;

        base.Destroy();
    }

    protected virtual void AddScorePlayer(int amount)
    {
        AddScore?.Invoke(amount);
    }
}
