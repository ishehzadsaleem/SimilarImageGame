using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardType : MonoBehaviour
{
    public List<CardSprite> cardTypeSprites;
    public Cards card;
    private void Awake()
    {
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
    public void SpawnCard(int size,CardType cardPrefab,Transform cardParent)
    {
        int numCardTypes = Enum.GetValues(typeof(Cards)).Length - 1;
        HashSet<Cards> uniqueCards = new HashSet<Cards>();
        List<CardType> instantiatedCards = new List<CardType>();
        Debug.Log("go through while until unique cards found");
        while (uniqueCards.Count < size)
        {
            int randomCardIndex = Random.Range(0, numCardTypes);
            Debug.Log("picking from enum list");
            Cards randomCardType = (Cards)Enum.ToObject(typeof(Cards), randomCardIndex);
            Debug.Log("random Card type "+randomCardType);
            card = randomCardType;
            if (uniqueCards.Add(randomCardType))
            {
                Sprite cardSprite = null;
                foreach (var item in cardTypeSprites)
                {
                    Debug.Log("iterating through the sprites list");
                    if (item.card == randomCardType)
                    {
                        cardSprite = item.sprite;
                        break;
                    }
                }
                if (cardSprite != null)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        CardType cardObject = Instantiate(cardPrefab);
                        cardObject.transform.GetChild(0).GetComponent<Image>().sprite = cardSprite;
                        instantiatedCards.Add(cardObject);
                        Debug.Log("instantiate each card two times to make a match, adding in a list and assign sprites");
                    }
                }
                else
                {
                    Debug.LogError("sprite not found");
                }
            }
        }
        instantiatedCards = instantiatedCards.OrderBy(x => Guid.NewGuid()).ToList();
        Debug.Log("shuffle list of cards to make it interesting");
        for (int i = 0; i < instantiatedCards.Count; i++)
        {
            instantiatedCards[i].transform.SetParent(cardParent);
        }
        Debug.Log("assigning to cards parent in canvas");
    }
}
public enum Cards : int
{
    Alligator,
    Bear,
    Elephant,
    Lion,
    Monkey,
    Moose,
    Owl,
    PolarBear,
    Rhino,
    Tiger,
    Count
}

[Serializable]
public struct CardSprite
{
    public Cards card;
    public Sprite sprite;
}
