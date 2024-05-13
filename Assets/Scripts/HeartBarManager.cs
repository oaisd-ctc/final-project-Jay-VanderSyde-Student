using UnityEngine;
using UnityEngine.UI;

public class HeartBarManager : MonoBehaviour
{
    public GameObject heartPrefab; // Prefab for a single heart UI element
    public Transform heartBarTransform; // Transform of the heart bar container
    public PlayerHealth playerHealth; // Reference to the player's health script

    private GameObject[] hearts; // Array to hold heart UI elements

    void Start()
    {
        InitializeHearts();
    }

    void Update()
    {
        UpdateHearts();
    }

    void InitializeHearts()
    {
        hearts = new GameObject[playerHealth.maxHealth]; // Create an array to hold heart UI elements
        for (int i = 0; i < playerHealth.maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartBarTransform); // Instantiate a heart UI prefab
            hearts[i] = heart; // Add the heart UI element to the array
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < playerHealth.maxHealth; i++)
        {
            if (i < playerHealth.currentHealth)
            {
                // Enable heart UI element if the player's current health is greater than the index
                hearts[i].SetActive(true);
            }
            else
            {
                // Disable heart UI element if the player's current health is less than or equal to the index
                hearts[i].SetActive(false);
            }
        }
    }
}