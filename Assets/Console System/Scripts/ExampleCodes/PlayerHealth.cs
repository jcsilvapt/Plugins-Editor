using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float Health = 100f;
    public bool IsInvincible = false;

    public void TakeDamage(float amount)
    {
        if (IsInvincible)
            return;

        Health -= amount;
        Debug.Log($"Player took {amount} damage. Current health: {Health}");

        if (Health <= 0)
        {
            Debug.Log("Player has died.");
            // Handle player death (e.g., respawn, game over, etc.)
        }
    }
}
