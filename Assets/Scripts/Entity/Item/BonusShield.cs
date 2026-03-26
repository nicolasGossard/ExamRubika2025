using UnityEngine;

public class BonusShield : Bonus
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = FindFirstObjectByType<Player>();

            if (player != null)
            {
                player.CreateShield();
                TriggerBonus("SHIELD ACTIVATED!");
            }

            Destroy();
        }
    }
}
