using UnityEngine;

public class Enemy : Character
{
    [Header("Paramètres de l'ennemi")]

    [SerializeField] private GameObject bonusPrefab;
    
    private void Start()
    {
        float randomX = Pcg32.RangeFloat(limitsX.x, limitsX.y);
        transform.position = new Vector3(randomX, 0, limitsZ.x);
        isSpawned = true;
    }

    private void Update()
    {
        if (isSpawned)
        {
            Move();
        }
    }

    public override void Move()
    {
        transform.position += Vector3.back * speedEntity * Time.deltaTime;

        LimitPosition(transform.position);
    }

    protected override void LimitPosition(Vector3 position)
    {
        if (position.z < limitsZ.y)
        {
            Destroy();

            Player player = FindFirstObjectByType<Player>();

            if (player != null)
            {
                player.TakeDammage(1);
            }
        }
    }

    protected override void Destroy()
    {
        if (Pcg32.NextFloat() < 0.5f)
        {
            Instantiate(bonusPrefab, transform.position, Quaternion.identity);
        }

        base.Destroy();
    }
}
