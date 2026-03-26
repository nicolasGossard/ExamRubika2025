using UnityEngine;

public class Asteroid : Character
{
    [Header("Paramètres de l'asteroid")]

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
        // Direction aléatoire pour chaque astéroïde
        float randomX = Pcg32.RangeFloat(-0.5f, 0.5f);

        Vector3 movement = new Vector3(randomX, 0, -1) * speedEntity * Time.deltaTime;
        transform.position += movement;
        transform.Rotate(0, 30 * Time.deltaTime, 0);

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
}
