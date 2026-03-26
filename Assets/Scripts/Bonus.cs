using System.Collections;
using UnityEngine;

public class Bonus : Entity
{
    [SerializeField] private float aliveTime;
    [SerializeField] private Renderer renderer;

    private void Start()
    {
        if (renderer == null)
        {
            renderer = gameObject.GetComponent<Renderer>();
        }
        renderer.enabled = true;
        
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
            renderer.enabled = true;
            yield return new WaitForSeconds(elapsedTime);
            renderer.enabled = true;
        }

        Destroy();
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
        }
    }

    protected override void Destroy()
    {
        base.Destroy();
    }
}
