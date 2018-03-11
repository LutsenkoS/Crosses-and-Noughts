using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Sides { Crosses, Noughts }
public class GameController : MonoBehaviour {

    private Toggle crosses;
    public static GameController Instance
    {
        get { return instance; }
    }
    public Sides playerSide
    {
        get { return side; }
    }
    
    private static GameController instance = null;
        
    private Sides side;
    void Awake()
    {
        //singleton pattern
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        GetSide();
    } 
    public void GetSide()
    {
        crosses = GameObject.FindGameObjectWithTag("Crosses").GetComponent<Toggle>();
        if (crosses.isOn)
            side = Sides.Crosses;
        else
            side = Sides.Noughts;
    }
   
}
