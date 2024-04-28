using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _gridLayoutGroupRow,_gridLayoutGroupColumn;
    [Range(2, 5)] public int rowCount;
    [Range(2, 7)] public int columnCount;
    private int higherCount;
    public Transform cardParent;
    public CardType cardType;
    public List<GameObject> selectedCards = new List<GameObject>();
    public int matchesCount;
    public Text textMatchesCount;
    public int turnsCount;
    public Text textTurnsCount;
    public AudioClip soundSelect;
    public AudioClip soundCorrect;
    public AudioClip soundWrong;
    public AudioClip winGame;
    public AudioSource audioSource;
    public Button nextButton;
    public bool isGameOver;
    void Start()
    {
        ChooseGridLayout();
        cardType.SpawnCard(higherCount,cardType,cardParent);
    }

    private void Update()
    {
        if (cardParent.transform.childCount <= 0 && isGameOver == false)
        {
            isGameOver = true;
            nextButton.interactable = true;
            audioSource.PlayOneShot(winGame);
        }
    }

    void ChooseGridLayout()
    {
        int tempRowCount = Random.Range(2, rowCount);
        _gridLayoutGroupRow.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        _gridLayoutGroupRow.constraintCount = tempRowCount;
        int tempColCount = Random.Range(2, columnCount);
        higherCount = tempRowCount*tempColCount;
        if (higherCount%2 !=0)
        {
            tempRowCount += 1;
            higherCount = (tempRowCount*tempColCount)/2;
        }
        else
        {
            higherCount = (tempRowCount*tempColCount)/2;
        }
        _gridLayoutGroupColumn.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroupColumn.constraintCount = tempColCount;
    }
    public void NextGame()
    {
        Debug.Log("game win");
        isGameOver = false;
        nextButton.interactable = false;
        turnsCount = 0;
        matchesCount = 0;
        textTurnsCount.text = turnsCount.ToString();
        textMatchesCount.text = matchesCount.ToString();
        ChooseGridLayout();
        cardType.SpawnCard(higherCount,cardType,cardParent);
    }
}
