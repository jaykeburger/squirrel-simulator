using UnityEngine;

public class PlayerSpawnHandler : MonoBehaviour
{
    private void Start()
    {
        // Restore the player's position if it has been set
        if (PlayerPosition.lastPlayerPosition != Vector3.zero)
        {
            Vector3 offset = (PlayerPosition.lastPlayerPosition.z < -50.0f) ? new Vector3(10, 0, 10) : new Vector3(10, 0, -10);
            transform.position = PlayerPosition.lastPlayerPosition + offset;
        }
    }
}
