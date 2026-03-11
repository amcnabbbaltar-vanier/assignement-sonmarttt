using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float timer = 0f;
    public bool timerRunning = true;
    private float timerAtLevelStart = 0f;

    public int score = 0;
    private int scoreAtLevelStart = 0;

    public int health = 3;

    public Canvas hudCanvas;

    [Header("HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI healthText;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 4 || scene.buildIndex == 0){ //end screen or main menu
            hudCanvas.enabled = false; //  → hide
            timerRunning = false;
        }
        else {
            hudCanvas.enabled = true;  // any level → show
            timerRunning = true;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            hudCanvas.enabled = false; 
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (timerRunning)
            timer += Time.deltaTime;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (healthText != null)
            healthText.text = "Health: " + health;

        if (timerText != null)
        {
            int minutes = (int)(timer / 60);
            int seconds = (int)(timer % 60);
            timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }
    }

    public void OnLevelStart() { scoreAtLevelStart = score; timerAtLevelStart = timer; }
    public (int, float) GetLevelStartScoreAndTimer() { return (scoreAtLevelStart, timerAtLevelStart); }
    public void AddScore(int amount) { score += amount; }
    public void StopTimer() { timerRunning = false; }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            health = 3;
            score = scoreAtLevelStart;
            timer = timerAtLevelStart;
            ResetGame();
        }
    }

    

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        score = scoreAtLevelStart;
        health = 3;
        timer = timerAtLevelStart;
        timerRunning = true;
    }
    public void Start()
    {
        score = 0;
        health = 3;
        timer = 0f;
        timerRunning = true;
        OnLevelStart();
    }
}