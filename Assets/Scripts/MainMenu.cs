using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //reset everything
        GameManager.Instance.score = 0;
        GameManager.Instance.health = 3;
        GameManager.Instance.timer = 0f;
        GameManager.Instance.timerRunning = true;
        SceneManager.LoadScene(1); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
