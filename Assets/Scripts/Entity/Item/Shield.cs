using UnityEngine;

public class Shield : Item
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Asteroid"))
        {
            Entity entity = other.GetComponent<Entity>();

            if (entity != null)
            {
                entity.Destroy();
                TakeDammage(1);
            }
        }
    }
}
