using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Stats")]
    public int currentHealth;
    public int maxHealth = 50;
    public int currentGold = 0;

    [Header("Deck")]
    // We will use Integers (Card IDs) or Strings for now to identify cards
    public List<string> playerDeck = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeGame()
    {
        // Setup initial stats for a new run
        currentHealth = maxHealth;
        currentGold = 100;
        
        // Add some starter cards (IDs)
        playerDeck.Add("attack_basic");
        playerDeck.Add("attack_basic");
        playerDeck.Add("attack_basic");
        playerDeck.Add("defense_basic");
        playerDeck.Add("defense_basic");
        
        Debug.Log("Game Initialized. HP: " + currentHealth);
    }

    // Call this when player takes damage
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took " + amount + " damage. Current HP: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log("Player healed. Current HP: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("GAME OVER - Player HP reached 0");
        
        // Call the SceneController to switch scenes
        if (SceneController.Instance != null)
        {
            SceneController.Instance.LoadGameOver();
        }
    }

    // NEW METHOD: Call this when clicking "Try Again"
    public void ResetRun()
    {
        // Reset Stats
        currentHealth = maxHealth;

        // This deletes the saved map state from the computer's memory
        PlayerPrefs.DeleteKey("Map");
        PlayerPrefs.Save();
        
        // Reset Deck (Optional: logic to reset to starter deck)
        playerDeck.Clear();
        playerDeck.Add("attack_basic");
        playerDeck.Add("attack_basic");
        playerDeck.Add("attack_basic");
        playerDeck.Add("defense_basic");
        playerDeck.Add("defense_basic");
        
        // Go back to Map
        if (SceneController.Instance != null)
        {
            SceneController.Instance.ReturnToMap();
        }
    }
}