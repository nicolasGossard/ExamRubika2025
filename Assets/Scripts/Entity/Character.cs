using UnityEngine;

public abstract class Character : Entity
{
    [Header("Paramètres du character")]

    [SerializeField] protected float speedEntity;
    [SerializeField] protected Vector2 limitsX;
    [SerializeField] protected Vector2 limitsZ;
    
    public abstract void Move();
    protected abstract void LimitPosition(Vector3 position);
}
