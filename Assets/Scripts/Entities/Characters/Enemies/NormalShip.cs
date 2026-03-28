using Unity.VisualScripting;
using UnityEngine;

public class NormalShip : Enemy
{
    [Header("Paramètres du vaisseau")]

    [SerializeField] private GameObject[] bonusPrefab;

    private void Update()
    {
        if (isSpawned)
        {
            Move();
        }
    }

    protected override void Move()
    {
        transform.position += Vector3.back * speedEntity * Time.deltaTime;

        LimitPosition(transform.position);
    }

    public override void Destroy()
    {
        CreateBonus();
        base.Destroy();
    }

    private void CreateBonus()
    {
        if (Pcg32.NextFloat() < 0.5f)
        {
            int randomNumber = Pcg32.RangeInt(100);

            if (randomNumber <= 15)
            {
                Instantiate(bonusPrefab[0], transform.position, Quaternion.identity);
            }
            else if (randomNumber <= 50)
            {
                Instantiate(bonusPrefab[1], transform.position, Quaternion.identity);

                Player player = FindFirstObjectByType<Player>();
            }
            else
            {
                Instantiate(bonusPrefab[2], transform.position, Quaternion.identity);
            }
        }
    }
}
