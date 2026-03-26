using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Paramètres de l'entité")]

    [SerializeField] protected string nameEntity;
    [SerializeField] protected int livesEntity;

    protected bool isSpawned = false;

    public int GetLives()
    {
        return livesEntity;
    }

    public virtual void TakeDammage(int amount)
    {
        livesEntity -= amount;

        if (livesEntity <= 0)
        {
            Destroy();
        }
    }

    public virtual void Destroy()
    {
       Destroy(gameObject);
    }
}
