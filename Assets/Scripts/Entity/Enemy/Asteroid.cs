using UnityEngine;

public class Asteroid : Enemy
{
    public static event System.Action<GameObject> OnAsteroidDestroyed;

    [Header("Paramètres de l'asteroid")]

    private Vector3 rotationAxis;
    private float rotationSpeed;

    protected override void Start()
    {
        base.Start();

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

    protected override void Move()
    {
        // Direction aléatoire pour chaque astéroïde
        float randomX = Pcg32.RangeFloat(-0.5f, 0.5f);

        Vector3 movement = new Vector3(randomX, 0, -1) * speedEntity * Time.deltaTime;
        transform.position += movement;
        transform.Rotate(0, 30 * Time.deltaTime, 0);

        LimitPosition(transform.position);
    }

    private void Turn()
    {
        if (Pcg32.NextFloat() < 0.5f)
        {
            transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
        }
    }

    public override void Destroy()
    {
        OnAsteroidDestroyed?.Invoke(gameObject);

        isSpawned = false;
        enabled = false;

        base.Destroy();
    }
}
