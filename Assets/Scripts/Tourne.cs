using UnityEngine;

public class Tourne : MonoBehaviour
{
    private Vector3 dir;
    private float vitesse;

    void Start()
    {
        dir = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        vitesse = Random.Range(0.001f, 0.01f);
        float skal = Random.Range(0.5f, 2f);
        transform.localScale = new Vector3(skal, skal, skal);
    }

    void Update()
    {
        transform.Rotate(dir, vitesse);
    }
}
