using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    [Header("Scene References")]
    public Transform handArea;      // Where cards spawn
    public GameObject cardPrefab;   // The UI Card Prefab
    public Enemy currentEnemy;      // Drag the 'EnemyUnit' here
    public Button endTurnButton;    // Drag the 'End Turn' button here
    public TextMeshProUGUI energyText; // Drag the 'Energy' text here

    [Header("Combat State")]
    public int maxEnergy = 3;
    public int currentEnergy;
    public bool isPlayerTurn = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Setup Button listener via code
        if(endTurnButton != null)
            endTurnButton.onClick.AddListener(EndPlayerTurn);
            
        StartCombat();
    }

    private void StartCombat()
    {
        Debug.Log("--- Combat Started ---");
        
        // Initialize Turn
        isPlayerTurn = true;
        currentEnergy = maxEnergy;
        UpdateEnergyUI();
        
        // Draw initial hand
        DrawCards(4); 
    }

    // Called from CardUI.cs
    public void PlayCard(CardData card, GameObject cardObject)
    {
        // 1. Validation: Is it player turn?
        if (!isPlayerTurn) 
        {
            Debug.LogWarning("Not your turn!");
            return;
        }

        // 2. Validation: Do we have enough energy?
        if (currentEnergy < card.energyCost)
        {
            Debug.LogWarning("Not enough Energy!");
            // TODO: Add visual feedback (shake screen, sound)
            return;
        }

        // 3. Pay Cost
        currentEnergy -= card.energyCost;
        UpdateEnergyUI();

        // 4. Execute Card Logic
        Debug.Log($"Playing Card: {card.cardName}");

        // A) Attack Logic
        if (card.damageAmount > 0)
        {
            if (currentEnemy != null)
            {
                currentEnemy.TakeDamage(card.damageAmount);
            }
        }

        // B) Block Logic
        if (card.blockAmount > 0)
        {
            // TODO: Implement proper Block System in GameManager
            // For Alpha, we just Log it or Heal slightly as a placeholder
            Debug.Log($"Player gained {card.blockAmount} Block (Logic pending)");
            
            // Temporary Hack: Heal player a bit to simulate defense
            if(GameManager.Instance != null) 
            {
                // Healing half the block amount just to see an effect
                // GameManager.Instance.Heal(card.blockAmount / 2); 
            }
        }

        // 5. Discard Card (Destroy the UI object)
        Destroy(cardObject);
    }
    
    // Called by the "End Turn" button
    public void EndPlayerTurn()
    {
        if (!isPlayerTurn) return;

        Debug.Log("--- Player Turn Ended ---");
        isPlayerTurn = false;
        
        // Discard remaining hand (Slay the Spire style)
        foreach (Transform child in handArea)
        {
            Destroy(child.gameObject);
        }

        // Trigger Enemy Turn with a slight delay for pacing
        Invoke("EnemyTurnAction", 1.0f);
    }

    private void EnemyTurnAction()
    {
        // Check if enemy is alive before attacking
        if (currentEnemy != null && currentEnemy.currentHp > 0)
        {
            currentEnemy.PerformTurn(); // Enemy deals damage to player
        }

        // Pass turn back to Player
        Invoke("StartPlayerTurn", 1.0f);
    }

    private void StartPlayerTurn()
    {
        Debug.Log("--- Player Turn Started ---");
        isPlayerTurn = true;
        
        // Reset Energy
        currentEnergy = maxEnergy; 
        UpdateEnergyUI();
        
        // Draw new hand
        DrawCards(5); 
    }

    private void DrawCards(int amount)
    {
        if (GameManager.Instance != null)
        {
            List<string> deck = GameManager.Instance.playerDeck;
            
            // Simple draw logic: always draw from the start of the deck
            // Future: Implement DrawPile and DiscardPile lists
            for (int i = 0; i < amount && i < deck.Count; i++)
            {
                SpawnCard(deck[i]);
            }
        }
    }

    private void SpawnCard(string cardId)
    {
        CardData data = CardDatabase.Instance.GetCardById(cardId);
        if (data != null)
        {
            GameObject newCard = Instantiate(cardPrefab, handArea);
            CardUI ui = newCard.GetComponent<CardUI>();
            ui.Initialize(data);
        }
    }

    private void UpdateEnergyUI()
    {
        if (energyText != null)
            energyText.text = $"Energy: {currentEnergy}/{maxEnergy}";
    }
    
    // Called by Enemy.cs when HP <= 0
    public void OnEnemyDeath()
    {
        Debug.Log("VICTORY! Returning to Map...");
        
        // Wait 2 seconds so player sees the enemy die, then load Map
        Invoke("ReturnToMap", 2.0f);
    }
    
    private void ReturnToMap()
    {
        if (SceneController.Instance != null)
        {
            SceneController.Instance.ReturnToMap();
        }
        else
        {
            Debug.LogError("SceneController not found!");
        }
    }
}