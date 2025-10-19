using System.Collections;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType { Jump, Speed }
    public PowerupType Type;

    [SerializeField] private int Duration = 5; // seconds

    private void OnTriggerEnter(Collider other)
    {
        // Only trigger if player touches it
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player == null) return;

        switch (Type)
        {
            case PowerupType.Jump:
                player.StartCoroutine(player.HandleJumpBoost(Duration));
                break;

            case PowerupType.Speed:
                player.StartCoroutine(player.HandleSpeedBoost(Duration));
                break;
        }

        Destroy(gameObject); // remove powerup after pickup
    }
}
