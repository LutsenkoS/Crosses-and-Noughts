using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateUI : MonoBehaviour {

	public void StartGame()
    {
        GameController.Instance.GetSide();
        SceneManager.LoadScene("Game");  
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
