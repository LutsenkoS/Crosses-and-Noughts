using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIPlayer : MonoBehaviour {
    public Sprite crosses;
    public Sprite noughts;

    private Sprite sideSprite;
    private int side;
    
    void Start()
    {
        if (GameController.Instance.playerSide == Sides.Crosses)
        {
            side = 1;
            sideSprite = noughts;
        }
        else
        {
            side = 0;
            sideSprite = crosses;
        }
    }
	void Update()
    {
        if (Game.Turn == 1)
        {
            StartCoroutine(AITurn());
            //now it's player turn
            Game.Turn = 0;
            //AITurn();
        }
    }
    IEnumerator waitTurn()
    {
        yield return new WaitForSeconds(2.0f);
    }
    private IEnumerator AITurn()
    {
        yield return new WaitForSeconds(0.1f);
        //check empty cells and fill random one
        Dictionary<int, int> emptyCells = new Dictionary<int, int>();
        Dictionary<int, int> AICells = new Dictionary<int, int>();
        byte[] emptyCellsArray;
        int i = 0;
        foreach (var cell in Field.cellsValue)
        {
            if (cell.Value == -1)
                emptyCells.Add(cell.Key, cell.Value);
            if (cell.Value == side)
                AICells.Add(cell.Key, cell.Value);
        }
        emptyCellsArray = new byte[emptyCells.Count];
        foreach (var cell in emptyCells)
        {
            emptyCellsArray[i] = (byte)cell.Key;
            i++;
        }
        System.Random rand = new System.Random();
        int k = emptyCells.Keys.ToList()[rand.Next(emptyCells.Count)];
        DrawCell(k);
       
       
    }

    private void DrawCell(int k)
    {
        Transform cell = Field.cells[k];
        cell.GetComponent<SpriteRenderer>().sprite = sideSprite;
        //this cell is not empty anymore
        Field.cellsValue[k] = side;
    }
}
