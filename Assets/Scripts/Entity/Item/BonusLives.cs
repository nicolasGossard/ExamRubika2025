using UnityEngine;

public class BonusLives : Bonus
{
    public static event System.Action OnBonusLivesCreated;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.AddLives(1);
                OnBonusLivesCreated?.Invoke();
            }

            Destroy();
        }
    }
}
