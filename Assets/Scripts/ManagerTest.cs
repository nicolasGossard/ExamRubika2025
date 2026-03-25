using Unity.VisualScripting;
using UnityEngine;

public class ManagerTest : MonoBehaviour
{
    [SerializeField] private Player player;

    void Start()
    {
        if (player == null)
            player = new Player();
    }

    // Update is called once per frame
    void Update()
    {
        player.Move();
    }
}
