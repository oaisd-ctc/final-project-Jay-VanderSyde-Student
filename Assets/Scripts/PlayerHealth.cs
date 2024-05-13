using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health of the player
    public int currentHealth; // Current health of the player
    public Text healthText; // Text to display player's health
    public GameObject loseScreen; // Game over panel to be activated when player loses all hearts

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Function to decrease player's health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    // Function to update player's health UI
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }

    // Function to handle game over
    void GameOver()
    {
        // Activate game over panel
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
        }
        // You can add more actions here such as showing a game over screen, resetting the level, etc.
    }
}