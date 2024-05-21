using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public GameObject heartPrefab;
    public Transform heartContainer;

    public GameObject gameOverScreen;

    private Image[] hearts;

    private void Start()
    {
        currentHealth = maxHealth;
        hearts = new Image[maxHealth];
        InitializeHearts();
    }

    private void InitializeHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heartObject = Instantiate(heartPrefab, heartContainer);
            hearts[i] = heartObject.GetComponent<Image>();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].enabled = true; // Show heart
            }
            else
            {
                hearts[i].enabled = false; // Hide heart
            }
        }
    }
}
