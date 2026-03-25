using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Paramètres du character")]

    [SerializeField] protected string nameCharacter;
    [SerializeField] protected int livesCharacter;
    [SerializeField] protected int speedCharacter;
    [SerializeField] protected Vector2 limitsX;
    [SerializeField] protected Vector2 limitsZ;

    protected bool isSpawned = false;

    public int GetLives()
    {
        return livesCharacter;
    }

    public virtual void TakeDammage(int amount)
    {
        livesCharacter -= amount;

        if (livesCharacter <= 0)
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
