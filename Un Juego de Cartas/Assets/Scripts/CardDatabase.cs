using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance;
    public List<CardData> allCards;
    private Dictionary<string, CardData> cardDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDictionary()
    {
        cardDictionary = new Dictionary<string, CardData>();
        
        foreach (var card in allCards)
        {
            if (!cardDictionary.ContainsKey(card.id))
            {
                cardDictionary.Add(card.id, card);
            }
            else
            {
                Debug.LogWarning($"Duplicate Card ID found: {card.id}");
            }
        }
        Debug.Log($"Card Database loaded with {cardDictionary.Count} cards.");
    }

    public CardData GetCardById(string id)
    {
        if (cardDictionary.ContainsKey(id))
        {
            return cardDictionary[id];
        }
        Debug.LogError($"Card with ID '{id}' not found in Database!");
        return null;
    }
}