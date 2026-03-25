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

    public virtual void TakeDammage(int amount)
    {
        livesCharacter -= amount;

        if (livesCharacter <= 0)
        {
            Destroy();
        }
    }

    public virtual void Destroy()
    {
       Destroy(this);
    }

    public abstract void Move();
    protected abstract void LimitPosition(Vector3 position);
    // protected abstract void Spawn();
}
