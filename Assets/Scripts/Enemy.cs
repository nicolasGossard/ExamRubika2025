using UnityEngine;

public class Enemy : Entity
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
        transform.position += Vector3.back * speedEntity * Time.deltaTime;

        LimitPosition(transform.position);
    }

    protected override void LimitPosition(Vector3 position)
    {
        if (position.z < limitsZ.y)
        {
            Destroy();
        }
    }
}
