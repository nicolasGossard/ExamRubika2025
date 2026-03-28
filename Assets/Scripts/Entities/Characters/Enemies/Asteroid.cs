using UnityEngine;

public class Asteroid : Enemy
{
    public static event System.Action<GameObject> OnAsteroidDestroyed;

    [Header("Paramètres de l'asteroid")]

    private float rotationSpeed;
    private Vector3 rotationAxis;

    protected override void Start()
    {
        base.Start();

        rotationSpeed = Pcg32.RangeFloat(50.0f, 300.0f);
        rotationAxis = Random.onUnitSphere;
    }

    private void Update()
    {
        if (isSpawned)
        {
            Move();
            Turn();
        }
    }

    protected override void Move()
    {
        // Direction aléatoire pour chaque astéroïde
        float randomX = Pcg32.RangeFloat(-0.5f, 0.5f);

        Vector3 movement = new Vector3(randomX, 0, -1) * speedEntity * Time.deltaTime;
        transform.position += movement;

        LimitPosition(transform.position);
    }

    private void Turn()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }

    public override void Destroy()
    {
        OnAsteroidDestroyed?.Invoke(gameObject);

        isSpawned = false;
        enabled = false;

        base.Destroy();
    }
}
