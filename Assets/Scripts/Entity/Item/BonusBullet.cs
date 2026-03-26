using UnityEngine;

public class BonusBullet : Bonus
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

                TriggerBonus(player.bulletCount == player.GetBulletMaxCount ?
                                       "MAX WEAPON LEVEL!  +200 SCORE" :
                                       "WEAPON UPGRADED!  BULLETS: " + player.bulletCount);
            }

            Destroy();
        }
    }
}
