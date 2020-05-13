using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void ContinueGame()
    {
        if (GameController.Control.CanLoadGame())
        {
            GameController.Control.NewGame = false;
            SceneManager.LoadScene(GameController.Control.GetLastScene());
        }
    }

    public void NewGame()
    {
        GameController.Control.NewGame = true;
        SceneManager.LoadScene(2);
    }
}
