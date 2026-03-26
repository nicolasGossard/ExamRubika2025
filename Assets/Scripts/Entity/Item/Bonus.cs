using UnityEngine;

public abstract class Bonus : Item
{
    public static event System.Action<string> OnBonusCreated;

    protected void TriggerBonus(string message)
    {
        OnBonusCreated?.Invoke(message);
    }
}
