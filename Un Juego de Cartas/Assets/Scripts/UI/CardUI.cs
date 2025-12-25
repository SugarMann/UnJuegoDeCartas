using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI descriptionText;
    // public Image artworkImage; // Uncomment if you have artwork

    private CardData myCardData;

    // Setup the card visuals
    public void Initialize(CardData data)
    {
        myCardData = data;
        
        nameText.text = data.cardName;
        costText.text = data.energyCost.ToString();
        descriptionText.text = data.description;
        
        // if (artworkImage != null) artworkImage.sprite = data.artwork;
    }

    // Call this when the button is clicked
    public void OnCardClicked()
    {
        // Pass the Card Data AND the GameObject itself to the Manager
        // Logic: Manager checks energy -> Applies effect -> Destroys this GameObject
        CombatManager.Instance.PlayCard(myCardData, gameObject);
    }
}