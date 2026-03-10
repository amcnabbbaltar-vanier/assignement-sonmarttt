using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI healthText;

    void Awake()
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

    void Update()
    {
        if (GameManager.Instance != null) return;
        if (scoreText == null || timerText == null || healthText == null) return;
        scoreText.text = "Score: " + GameManager.Instance.score;
        healthText.text = "Health: " + GameManager.Instance.health;

        float t = GameManager.Instance.timer;
        int minutes = (int)(t / 60);
        int seconds = (int)(t % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        
    }

}
