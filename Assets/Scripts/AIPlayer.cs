using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIPlayer : MonoBehaviour
{
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
            //StartCoroutine(AITurn());
            AITurn();
            //now it's player turn
            Game.Turn = 0;
            //AITurn();
        }
    }

    private void AITurn()
    {
        //yield return new WaitForSeconds(0.1f);
        //check empty cells and fill random one
        //Dictionary<int, int> emptyCells = new Dictionary<int, int>();
        List<int> emptyCells = new List<int>();
        List<int> playerCells = new List<int>();
        //Dictionary<int, int> AICells = new Dictionary<int, int>();
        List<int> AICells = new List<int>();
        System.Random rand = new System.Random();
        List<int> temp = new List<int>();
        foreach (var cell in Field.cellsValue)
        {
            if (cell.Value == -1)
                emptyCells.Add(cell.Key);
            else
                if (cell.Value == side)
                AICells.Add(cell.Key);
            else
                playerCells.Add(cell.Key);
        }
        emptyCells.Sort();
        AICells.Sort();
        playerCells.Sort();
        //if AI has 2 from 3 fill last one
        if (TwoFromThree(AICells, emptyCells))       
            return;        
        //if player has 2 from 3 interrupt him
        if (TwoFromThree(playerCells, emptyCells))       
            return;        
        //if centre cell is empty fill it
        if (emptyCells.Contains(4))
        {
            DrawCell(4);
            return;
        }
        
        temp = emptyCells.Intersect(new List<int>(new int[4] { 0, 2, 6, 8 })).ToList();
        //fill random corner cell
        if (temp.Count() > 0)
        {
            DrawCell(temp[rand.Next(temp.Count())]);
            return;
        }       
        //fill random cell
        DrawCell(emptyCells[rand.Next(emptyCells.Count)]);

    }

    private bool TwoFromThree(List<int> comb, List<int> emptyCells)
    {
        List<int> temp = new List<int>();
        foreach (var template in Game.templateList)
        {
            temp = template.Intersect(comb).ToList();
            if (temp.Count() == 2)
            {
                var thirdCell = template.Except(temp).FirstOrDefault();
                if (emptyCells.Contains(thirdCell))
                {                   
                    DrawCell(thirdCell);
                    return true;
                }
            }
        }
        return false;
    }
    private void DrawCell(int k)
    {
        Transform cell = Field.cells[k];
        cell.GetComponent<SpriteRenderer>().sprite = sideSprite;
        //this cell is not empty anymore
        Field.cellsValue[k] = side;
    }
}
