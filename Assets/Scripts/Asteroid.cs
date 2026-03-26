using UnityEngine;

public class Asteroid : Character
{
    [Header("Paramètres de l'asteroid")]

    [SerializeField] private GameObject bonusPrefab;

    private Vector3 rotationAxis;
    private float rotationSpeed;

    private void Start()
    {
        float randomX = Pcg32.RangeFloat(limitsX.x, limitsX.y); 
        transform.position = new Vector3(randomX, 0, limitsZ.x);
        isSpawned = true;

        rotationAxis = Random.onUnitSphere;
        rotationSpeed = Random.Range(1.0f, 2.0f);
    }

    private void Update()
    {
        if (isSpawned)
        {
            Move();
            Turn();
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

    private void Turn()
    {
        if (Pcg32.NextFloat() < 0.5f)
        {
            transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
        }
    }
}
