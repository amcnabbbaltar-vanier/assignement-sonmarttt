using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float timer = 0f;
    public bool timerRunning = true;

    public int score = 0;
    private int scoreAtLevelStart = 0;
    public int GetLevelStartScore()
    {
        return scoreAtLevelStart;
    }

    public int health = 3;

    public void OnLevelStart()
    {
        scoreAtLevelStart = score;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    void Update()
    {
        if (timerRunning)
            timer += Time.deltaTime;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            health = 3; 
            score = scoreAtLevelStart;
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
