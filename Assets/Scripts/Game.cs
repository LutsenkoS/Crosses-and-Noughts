using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Game : MonoBehaviour {
    //if zero it's player turn, if 1 - AI turn
    public static int Turn;
    public Sprite win;
    public Sprite lose;
    public Sprite draw;
    public GameObject hint;

    public static List<List<int>> templateList = new List<List<int>>() { new List<int>(new int[3] { 0, 1, 2 }), new List<int>(new int[3] { 3, 4, 5 }),
        new List<int>(new int[3] { 6, 7, 8 }), new List<int>(new int[3] { 0, 3, 6 }), new List<int>(new int[3] { 1, 4, 7 }),
        new List<int>(new int[3] { 2, 5, 8 }), new List<int>(new int[3] { 0, 4, 8 }), new List<int>(new int[3] { 2, 4, 6 })};

    private LineRenderer line;
    public bool GameOver
    {
        get { return gameOver; }
        set
        {
            gameOver = value;
        }
    }

    private Sides AISide;

    //0 - player win, 1 - AI win
    private Sides playerSide;
    private int winner = -1;
    private bool gameOver;
    private List<int> winLine;
    
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        playerSide = GameController.Instance.playerSide;
        AISide = (playerSide == Sides.Crosses) ? Sides.Noughts : Sides.Crosses;
        //if player has crosses he goes first, otherwise second
        Turn = playerSide == Sides.Crosses ? 0 : 1;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
        if (!GameOver)
            CheckWin();
        if (!Field.cellsValue.ContainsValue(-1))
            GameOver = true;
        if (GameOver)
            StartCoroutine(OnGameOver());

    }

    private void CheckWin()
    {
        List<int> playerCells = new List<int>();
        List<int> AICells = new List<int>();
        foreach (var cell in Field.cellsValue)
        {
            if (cell.Value == (int)playerSide)
                playerCells.Add(cell.Key);
            else
            {
                if (cell.Value != -1)
                    AICells.Add(cell.Key);
            }
        }
        //Debug.Log(playerKeys.Count);
        playerCells.Sort();
        AICells.Sort();
        if (CheckLines(playerCells, playerSide))
            GameOver = true;
        if (CheckLines(AICells, AISide))
            GameOver = true;
    }

    private bool CheckLines(List<int> cells, Sides player )
    {
        
        foreach (var template in templateList)
        {
            if (cells.Intersect(template).Count() == template.Count())
            {
                winner = (int)player;
                winLine = template;
                return true;               
            }
        }
        return false;
      
    }
    private IEnumerator OnGameOver()
    {
        //nobody's turn
        Turn = -1;
        //draw win line
        if(winner != -1)
            line.SetPositions(new Vector3[] { Field.cells[winLine[0]].position, Field.cells[winLine[2]].position });
        yield return new WaitForSeconds(0.5f);
        hint.SetActive(true);
        if (winner == (int)playerSide)
        {          
            GameObject.FindGameObjectWithTag("GameOver").GetComponent<SpriteRenderer>().sprite = win;
        }
        else
            if(winner == (int)AISide)
                GameObject.FindGameObjectWithTag("GameOver").GetComponent<SpriteRenderer>().sprite = lose;
            else
                GameObject.FindGameObjectWithTag("GameOver").GetComponent<SpriteRenderer>().sprite = draw;

    }

}
