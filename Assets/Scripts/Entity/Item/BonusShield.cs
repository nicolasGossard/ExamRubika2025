using UnityEngine;

public class BonusShield : Bonus
{
    public static event System.Action OnBonusShieldCreated;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = FindFirstObjectByType<Player>();

            if (player != null)
            {
                player.CreateShield();
                OnBonusShieldCreated?.Invoke();
            }

            Destroy();
        }
    }
}
