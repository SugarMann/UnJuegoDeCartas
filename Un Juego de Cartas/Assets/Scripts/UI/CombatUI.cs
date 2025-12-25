using UnityEngine;
using TMPro; // Or using UnityEngine.UI if using legacy text

public class CombatUI : MonoBehaviour
{
    public TextMeshProUGUI healthText; // Drag your text here in Inspector

    private void Start()
    {
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        if (GameManager.Instance != null)
        {
            healthText.text = $"HP: {GameManager.Instance.currentHealth} / {GameManager.Instance.maxHealth}";
        }
    }
    
    // Test Button function
    public void TestDamage()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TakeDamage(10);
            UpdateHealthUI();
        }
    }
}
