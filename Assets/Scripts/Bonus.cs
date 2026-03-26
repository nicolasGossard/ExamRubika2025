using System.Collections;
using UnityEngine;

public class Bonus : Entity
{
    [SerializeField] private float aliveTime;
    [SerializeField] private Renderer bonusRenderer;

    private void Start()
    {
        if (bonusRenderer == null)
        {
            bonusRenderer = gameObject.GetComponent<Renderer>();
        }
        
        StartCoroutine(WaitToDestroy());
    }

    private void Update()
    {
        Move();
    }

    private IEnumerator WaitToDestroy()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < aliveTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bonusRenderer.enabled = false;
        yield return new WaitForSeconds(0.20f);
        bonusRenderer.enabled = true;
        yield return new WaitForSeconds(0.20f);
        bonusRenderer.enabled = false;
        yield return new WaitForSeconds(0.20f);
        bonusRenderer.enabled = true;

        Destroy();
    }

    public override void Move()
    {
        transform.position += Vector3.back * speedEntity * Time.deltaTime;

        LimitPosition(transform.position);
    }

    protected override void LimitPosition(Vector3 position)
    {
        if (position.z < limitsZ.x || position.z > limitsZ.y ||
            position.x < limitsX.x || position.x > limitsX.y)
        {
            Destroy();
        }
    }

    protected override void Destroy()
    {
        Debug.Log("Bonus detruit");
        base.Destroy();
    }
}
