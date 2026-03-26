using UnityEngine;

public class Bullet : Character
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Asteroid"))
        {
            Entity entity = other.GetComponent<Entity>();

            if (entity != null)
            {
                entity.TakeDammage(1);

                // Le missile aussi prend un de dégat quand il touche l'obstacle, et  
                // vu qu'il a un seul point de vie il se détruira à ce moment là
                TakeDammage(1);
            }
        }
    }
}
