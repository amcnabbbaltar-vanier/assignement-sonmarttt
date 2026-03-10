using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }
    //freezes the game
    public void PauseGame()
    {
        if (pauseMenuPanel == null)
        {
            Debug.LogError("pauseMenuPanel is not assigned on: " + gameObject.name);
            return;
        }
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        GameManager.Instance.score = GameManager.Instance.GetLevelStartScore();
        GameManager.Instance.health = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); 
    }
}
