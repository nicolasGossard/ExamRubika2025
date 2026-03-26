using UnityEngine;

public class BonusLives : Bonus
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.AddLives(1);
                TriggerBonus("LIFE UP! +1 LIFE");
            }

            Destroy();
        }
    }
}
