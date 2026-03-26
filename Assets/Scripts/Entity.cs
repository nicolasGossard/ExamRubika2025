using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Paramètres de l'entité")]

    [SerializeField] protected string nameEntity;
    [SerializeField] protected int livesEntity;
    [SerializeField] protected float speedEntity;
    [SerializeField] protected Vector2 limitsX;
    [SerializeField] protected Vector2 limitsZ;

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

    protected virtual void Destroy()
    {
       Destroy(gameObject);
    }

    public abstract void Move();
    protected abstract void LimitPosition(Vector3 position);
}
