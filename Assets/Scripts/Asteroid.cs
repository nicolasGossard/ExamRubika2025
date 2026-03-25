using UnityEngine;

public class Asteroid : Character
{
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

        Vector3 movement = new Vector3(randomX, 0, -1) * speedCharacter * Time.deltaTime;
        transform.position += movement;
        transform.Rotate(0, 30 * Time.deltaTime, 0);

        LimitPosition(transform.position);
    }

    protected override void LimitPosition(Vector3 position)
    {
        if (position.z < limitsZ.y)
        {
            Destroy();
        }
    }

    protected override void Destroy()
    {
        Player player = FindFirstObjectByType<Player>();
        player.TakeDammage(1);
        base.Destroy();
    }
}
