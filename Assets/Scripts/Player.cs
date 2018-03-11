using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Player : MonoBehaviour {

    public Sprite crosses;
    public Sprite noughts;

    private Vector2 clickPoint;
    private RaycastHit2D hit;
    private Sprite sideSprite;
    
    void Start()
    {
        if (GameController.Instance.playerSide == Sides.Crosses)
            sideSprite = crosses;
        else
            sideSprite = noughts;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Game.Turn == 0)
            {
                PlayerTurn();                
            }
        }       
    }
    private void PlayerTurn()
    {        
        clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(clickPoint, Vector2.zero);
        if (hit.collider)
        {
            Transform hitCell = hit.transform;
            if (!hitCell.GetComponent<SpriteRenderer>().sprite)
            {
                hitCell.GetComponent<SpriteRenderer>().sprite = sideSprite;
                CellsValueChange(hitCell);
            }
        }
    }
    private void CellsValueChange(Transform hitCell)
    {
        var key = Field.cells.Where(k => k.Value == hitCell).Select(k => k.Key).FirstOrDefault();
        Field.cellsValue[key] = (int)GameController.Instance.playerSide;
        Game.Turn = 1;
    }
}


