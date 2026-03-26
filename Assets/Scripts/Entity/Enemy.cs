using UnityEngine;

public class Enemy : Character
{
    [Header("Paramètres de l'ennemi")]

    [SerializeField] private GameObject[] bonusPrefab;
    
    private void Start()
    {
        float randomX = Pcg32.RangeFloat(limitsX.x, limitsX.y);
        transform.position = new Vector3(randomX, 0, limitsZ.x);
        isSpawned = true;
    }

    private void Update()
    {
        if (isSpawned)
        {
            Move();
        }
    }

    public override void Move()
    {
        transform.position += Vector3.back * speedEntity * Time.deltaTime;

        LimitPosition(transform.position);
    }

    protected override void LimitPosition(Vector3 position)
    {
        if (position.z < limitsZ.y)
        {
            Destroy();

            Player player = FindFirstObjectByType<Player>();

            if (player != null)
            {
                player.TakeDammage(1);
            }
        }
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
            else if (randomNumber <= 40)
            {
                Instantiate(bonusPrefab[1], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bonusPrefab[2], transform.position, Quaternion.identity);
            }
        }
    }

}
