using System.Collections;
using UnityEngine;

public class Player : Character
{
    [Header("Paramètres du Player")]

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shieldPrefab;

    public int bulletCount = 1;
    private float bulletSpacing = 0.5f;
    private int bulletMaxCount = 5;
    public float bulletTimer = 15.0f;

    public int GetBulletMaxCount => bulletMaxCount;

    public static event System.Action<int> OnLivesChanged;
    public static event System.Action OnPlayerDied;
    
    private void Start()
    {
        transform.position = new Vector3(0.0f, 0f, -8.0f);
        isSpawned = true;

        OnLivesChanged?.Invoke(livesEntity);
    }

    private void Update()
    {
        if (isSpawned)
        {
            Move();
            Fire();
        }
    }

    public void AddLives(int amount)
    {
        livesEntity += amount;
        OnLivesChanged?.Invoke(livesEntity);
    }

    public void AddBullet(int amount)
    {
        if ((bulletCount += amount) > bulletMaxCount)
        {
            bulletCount = bulletMaxCount;
        }

        StartCoroutine(BulletEffect());
    }

    public void RemoveBullet(int amount)
    {
        bulletCount = 1;
    }

    private IEnumerator BulletEffect()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < bulletTimer)
        {
            float t = elapsedTime / bulletTimer;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        RemoveBullet(1);
    }

    protected override void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // D�placement sur le plan XZ
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speedEntity * Time.deltaTime;
        transform.position += movement;

        // Calcul des angles de rotation pour les deux axes
        float tiltAngleZ = -horizontalInput * 30f; // Inclinaison lat�rale (gauche/droite)
        float tiltAngleX = verticalInput * 15f;    // Inclinaison longitudinale (avant/arri�re)

        // Cr�ation d'une rotation qui combine les deux inclinaisons
        Quaternion targetRotation = Quaternion.Euler(tiltAngleX, 0, tiltAngleZ);

        // Application de la rotation avec un lissage pour un effet plus naturel
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

        // Si aucun input, retour progressif � la rotation neutre
        if (horizontalInput == 0 && verticalInput == 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 5f * Time.deltaTime);
        }

        LimitPosition(transform.position);
    }

    protected override void LimitPosition(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, limitsX.x, limitsX.y);
        position.z = Mathf.Clamp(position.z, limitsZ.x, limitsZ.y);
        transform.position = position;
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // Calcul de la position de d�part pour centrer les projectiles
            float startX = -((bulletCount - 1) * bulletSpacing) / 2;

            // Cr�ation de plusieurs balles c�te � c�te
            for (int i = 0; i < bulletCount; i++)
            {
                // Calcule la position avec l'offset horizontal
                Vector3 bulletOffset = new Vector3(startX + (i * bulletSpacing), 0.0f, 0.5f);
                Vector3 spawnPosition = transform.position + bulletOffset;

                // Instanciation du projectile
                Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    public void CreateShield()
    {
        GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        shield.transform.parent = transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Asteroid"))
        {
            Destroy();
        }
    }

    public override void TakeDammage(int amount)
    {
        base.TakeDammage(amount);
        OnLivesChanged?.Invoke(livesEntity);
    }

    public override void Destroy()
    {
        OnLivesChanged?.Invoke(0);
        OnPlayerDied?.Invoke();
        base.Destroy();
    }
}
