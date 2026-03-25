using UnityEngine;

public class Player : Character
{
    public override void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // D�placement sur le plan XZ
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speedCharacter * Time.deltaTime;
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
}
