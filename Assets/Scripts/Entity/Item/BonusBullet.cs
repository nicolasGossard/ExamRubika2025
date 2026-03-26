using UnityEngine;

public class BonusBullet : Item
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = FindFirstObjectByType<Player>();

            if (player != null)
            {
                if (player.bulletCount++ > player.GetBulletMaxCount)
                {
                    player.bulletCount = player.GetBulletMaxCount;
                }
            }

            Destroy();
        }
    }
}
