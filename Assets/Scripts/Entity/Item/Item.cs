using System.Collections;
using UnityEngine;

public abstract class Item : Entity
{
    [Header("Paramètres de l'Item")]

    [SerializeField] private float itemTimer;
    [SerializeField] private Renderer itemRenderer;

    protected virtual void Start()
    {
        if (itemRenderer == null)
        {
            itemRenderer = gameObject.GetComponent<Renderer>();
        }
        
        StartCoroutine(WaitToDestroy());
    }

    private IEnumerator WaitToDestroy()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < itemTimer)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        itemRenderer.enabled = false;
        yield return new WaitForSeconds(0.20f);
        itemRenderer.enabled = true;
        yield return new WaitForSeconds(0.20f);
        itemRenderer.enabled = false;
        yield return new WaitForSeconds(0.20f);
        itemRenderer.enabled = true;

        Destroy();
    }
}
