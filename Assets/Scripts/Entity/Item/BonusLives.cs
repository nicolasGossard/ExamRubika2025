using UnityEngine;

public class BonusLives : Item
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.AddLives(1);

            Destroy();
        }
    }
}
