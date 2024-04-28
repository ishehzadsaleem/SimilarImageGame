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
    public Animator _cardAnimationController;
    private CardManager _cardManager;
    public event Action OnGameComplete; 
    private void Awake()
    {
    }
    void Start()
    {
        _cardManager = FindObjectOfType<CardManager>();
        
        

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
            instantiatedCards[i].GetComponent<Animator>().Play("Start Flipping");
        }
        Debug.Log("assigning to cards parent in canvas");
    }
    
    public void MatchCard()
    {
        Debug.Log("card type is "+card);
        _cardManager.audioSource.PlayOneShot(_cardManager.soundSelect);
        if (_cardManager.selectedCards.Count < 2)
        {
            _cardAnimationController.Play("Card Flip");
            GameObject pressedButton = EventSystem.current.currentSelectedGameObject;
            Debug.Log("pressed button "+pressedButton);
            pressedButton.GetComponent<Button>().interactable = false;
            _cardManager.selectedCards.Add(pressedButton);
        }
        if (_cardManager.selectedCards.Count == 2)
        {
            if (_cardManager.selectedCards[0].GetComponent<CardType>().card == _cardManager.selectedCards[1].GetComponent<CardType>().card)
            {
                _cardManager.matchesCount += 1;
                _cardManager.textMatchesCount.text = _cardManager.matchesCount.ToString();
                _cardManager.audioSource.PlayOneShot(_cardManager.soundCorrect);
                StartCoroutine(RemoveMatchedCard());
                Debug.Log("Cards Matched!");
            }
            else
            {

                Debug.Log("Cards not Matched!");
                StartCoroutine(ResetSelectedCards());
            }
            _cardManager.turnsCount += 1;
            _cardManager.textTurnsCount.text = _cardManager.turnsCount.ToString();
        }

  
    }
    
    IEnumerator ResetSelectedCards()
    {
        _cardManager.audioSource.PlayOneShot(_cardManager.soundWrong);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _cardManager.selectedCards.Count; i++)
        {
            _cardManager.selectedCards[i].GetComponent<Button>().interactable = true;
            _cardManager.selectedCards[i].GetComponent<Animator>().Play("Card Flip Reverse");
        }
        _cardManager.selectedCards.Clear();
    }
    IEnumerator RemoveMatchedCard()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _cardManager.selectedCards.Count; i++)
        {
            Destroy(_cardManager.selectedCards[i].gameObject);
        }
        _cardManager.selectedCards.Clear();
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
