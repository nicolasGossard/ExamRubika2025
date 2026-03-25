using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Paramètres du character")]

    [SerializeField] protected string nameCharacter;
    [SerializeField] protected int livesCharacter;
    [SerializeField] protected int speedCharacter;

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
}
