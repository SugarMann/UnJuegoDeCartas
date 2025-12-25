using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public string enemyName = "Cultist";
    public int maxHp = 20;
    public int currentHp;
    public int damagePerTurn = 6; // Fixed damage for Alpha version

    [Header("UI References")]
    public TextMeshProUGUI hpText; // Drag the enemy HP text here
    // public Image intentionIcon; // Future: Show intention (Attack/Block)

    private void Start()
    {
        currentHp = maxHp;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;
        if (currentHp < 0) currentHp = 0;
        
        Debug.Log($"{enemyName} took {amount} damage. Current HP: {currentHp}");
        
        // Simple Hit Animation (Visual Feedback)
        transform.localScale = Vector3.one * 0.9f; 
        Invoke("ResetScale", 0.1f);

        UpdateUI();

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void ResetScale()
    {
        transform.localScale = Vector3.one;
    }

    private void UpdateUI()
    {
        if (hpText != null)
            hpText.text = $"{enemyName}: {currentHp}/{maxHp}";
    }

    private void Die()
    {
        Debug.Log($"{enemyName} is Dead!");
        
        // Notify CombatManager about victory
        if (CombatManager.Instance != null)
        {
            CombatManager.Instance.OnEnemyDeath();
        }
        
        // Hide enemy visually
        gameObject.SetActive(false);
    }
    
    // Logic for the Enemy's Turn
    public void PerformTurn()
    {
        Debug.Log($"{enemyName} attacks for {damagePerTurn} damage!");
        
        // Deal damage to the Player via GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TakeDamage(damagePerTurn);
            
            // Hack: Find the CombatUI to update the player's health bar immediately
            FindObjectOfType<CombatUI>()?.UpdateHealthUI();
        }
    }
}