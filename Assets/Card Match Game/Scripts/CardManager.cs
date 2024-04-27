using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _gridLayoutGroupRow,_gridLayoutGroupColumn;
    [Range(2, 5)] public int rowCount;
    [Range(2, 7)] public int columnCount;
    private int higherCount;
    void Start()
    {
        ChooseGridLayout();
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
}
