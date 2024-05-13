using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 1; // Amount of damage the enemy deals to the player

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the contact points of the collision
            ContactPoint2D[] contacts = new ContactPoint2D[1];
            collision.GetContacts(contacts);

            // Check if the player collided with the top of the enemy
            if (IsPlayerOnTop(contacts[0].point))
            {
                // Destroy the enemy
                Destroy(gameObject);
            }
            else
            {
                // Deal damage to the player
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }
    }

    // Function to check if the player is on top of the enemy
    private bool IsPlayerOnTop(Vector2 contactPoint)
    {
        // Adjust this value according to the size of your enemy sprite
        float tolerance = 0.1f;

        // Compare the y position of the contact point with the enemy's position
        return contactPoint.y > transform.position.y + tolerance;
    }
}