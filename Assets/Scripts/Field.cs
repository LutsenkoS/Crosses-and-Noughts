using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Field : MonoBehaviour {

    public static int selectedCell;
   
    public Transform cell;
    public Transform cellParent;
    //-1 = empty, 0 = crosses, 1 = noughts
    public static Dictionary<int, int> cellsValue;    
    public static Dictionary<int, Transform> cells;

    private float x = -1.7f;
    private float y = -1.7f;
    void Start()
    {
        InitField();                        
    }

    private void InitField()
    {
        cellsValue = new Dictionary<int, int>();
        for (int i = 0; i < 9; i++)
        {
            cellsValue.Add(i, -1);
        }
        int row = 0;
        cells = new Dictionary<int, Transform>();

        for (int i = 0; i < 9; i++)
        {
            Transform obj = Instantiate(cell, new Vector3(x, y, 0), Quaternion.identity);
            obj.SetParent(cellParent);
            cells.Add(i, obj);
            x += 1.7f;
            if (i % 3 == 2)
            {
                row++;
                y += 1.7f;
                x = -1.7f;
            }

        }
    }
}
