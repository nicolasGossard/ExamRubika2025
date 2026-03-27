using UnityEngine;

public class Shield : Item
{
    public static event System.Action OnShieldBreaked;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Asteroid"))
        {
            Entity entity = other.GetComponent<Entity>();

            if (entity != null)
            {
                entity.Destroy();
                TakeDammage(1);
            }
        }
    }

    public override void Destroy()
    {
        OnShieldBreaked?.Invoke();
        base.Destroy();
    }
}
