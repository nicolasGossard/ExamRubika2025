using UnityEngine;

public class Shield : Item
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Asteroid"))
        {
            TakeDammage(1);
        }
    }
}
