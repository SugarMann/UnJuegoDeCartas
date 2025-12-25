using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Card Data")]
public class CardData : ScriptableObject
{
    [Header("Card Info")]
    public string id;           // Unique ID (e.g., "attack_basic")
    public string cardName;     // Display name (e.g., "Strike")
    [TextArea]
    public string description;  // Text shown on card
    public int energyCost;      // How much it costs to play
    public Sprite artwork;      // The image (optional for now)

    [Header("Values")]
    public int damageAmount;    // If 0, it doesn't attack
    public int blockAmount;     // If 0, it doesn't block

    // TODO: More effects (e.g., DrawCards, Heal)
}