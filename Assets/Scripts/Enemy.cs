using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform leftPoint;
    public Transform rightPoint;

    private bool movingRight = true;
    private PlayerHealth enemyHealth;

    private void Start()
    {
        enemyHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        // Move the enemy back and forth between left and right points
        if (transform.position.x >= rightPoint.position.x)
        {
            movingRight = false;
        }
        else if (transform.position.x <= leftPoint.position.x)
        {
            movingRight = true;
        }

        Vector3 movement = movingRight ? Vector3.right : Vector3.left;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the player
        if (other.CompareTag("Player"))
        {
            // Check if the player is jumping on the enemy
            if (IsPlayerJumpingOnEnemy(other))
            {
                if (enemyHealth != null)
                {
                    // Take away one heart from the enemy
                    enemyHealth.TakeDamage(1);

                    // Destroy the enemy if it loses all health
                    if (enemyHealth.currentHealth <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                // Get the PlayerHealth component from the player
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    // Take away one heart from the player
                    playerHealth.TakeDamage(1);
                }
            }
        }
    }

    private bool IsPlayerJumpingOnEnemy(Collider2D playerCollider)
    {
        // Calculate the overlap area between the enemy's collider and the player's collider
        Collider2D enemyCollider = GetComponent<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = false;
        Collider2D[] overlapResults = new Collider2D[5]; // Adjust the size as needed
        int numOverlaps = enemyCollider.OverlapCollider(contactFilter, overlapResults);

        for (int i = 0; i < numOverlaps; i++)
        {
            if (overlapResults[i] == playerCollider)
            {
                // Check if the player is above the enemy
                if (playerCollider.transform.position.y > transform.position.y)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
