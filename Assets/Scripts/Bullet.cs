using UnityEngine;

public class Bullet : Entity
{
    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        transform.position += Vector3.forward * speedEntity * Time.deltaTime;

        LimitPosition(transform.position);
    }

    protected override void LimitPosition(Vector3 position)
    {
        if (position.z > limitsZ.y)
        {
            Destroy();
        }
    }
}
