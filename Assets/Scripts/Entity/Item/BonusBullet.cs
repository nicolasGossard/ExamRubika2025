using UnityEngine;

public class BonusBullet : Bonus
{
    public static event System.Action OnBonusBulletCreated;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = FindFirstObjectByType<Player>();

            if (player != null)
            {
                player.AddBullet(1);
                OnBonusBulletCreated?.Invoke();
            }

            Destroy();
        }
    }
}
