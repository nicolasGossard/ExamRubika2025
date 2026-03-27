using UnityEngine;

public abstract class Enemy : Character
{
    public static event System.Action<int> AddScore;

    [SerializeField] private int enemyValue;

    protected virtual void Start()
    {
        float randomX = Pcg32.RangeFloat(limitsX.x, limitsX.y);
        transform.position = new Vector3(randomX, 0, limitsZ.x);
        isSpawned = true;
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

    protected virtual void AddScorePlayer(int amount)
    {
        AddScore?.Invoke(amount);
    }
}
