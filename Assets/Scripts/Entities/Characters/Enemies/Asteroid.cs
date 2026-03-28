using UnityEngine;

public class Asteroid : Enemy
{
    [Header("Paramètres de l'asteroid")]

    private float rotationSpeed;
    private Vector3 rotationAxis;
    
    private float size;

    protected override void Start()
    {
        base.Start();

        RandomStart();
    }

    private void RandomStart()
    {
        // On prépare un sens et une vitesse de rotation aléatoires

        rotationSpeed = Pcg32.RangeFloat(50.0f, 300.0f);

        // Ici c'est plus facile d'utiliser directement Random.onUnitSphere pour
        // obtenir une rotation aléatoire que d'utiliser mon PCG32
        rotationAxis = Random.onUnitSphere;

        // On prépare une taille est des points de vie dépendants et aléatoires
        // Petit asteroide : 1 PV, moyen asteroide : 2 PV, grand asteroide : 3 PV
        int randomSize = Pcg32.RangeInt(0, 3);

        if (randomSize == 0)
        {
            size = 0.8f;
            livesEntity = 1;
        }
        else if (randomSize == 1)
        {
            size = 1.2f;
            livesEntity = 2;
        }
        else
        {
            size = 1.6f;
            livesEntity = 3;
        }

        transform.localScale = new Vector3(size, size, size);
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
}
