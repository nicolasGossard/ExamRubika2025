using UnityEngine;

public class BonusShield : Item
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = FindFirstObjectByType<Player>();

            if (player != null)
            {
                player.CreateShield();
            }

            Destroy();
        }
    }
}
