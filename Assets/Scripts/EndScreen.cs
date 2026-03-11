using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalTimeText;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            finalScoreText.text = "Final Score: " + GameManager.Instance.score;

            float t = GameManager.Instance.timer;
            int minutes = (int)(t / 60);
            int seconds = (int)(t % 60);
            finalTimeText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);

            GameManager.Instance.StopTimer();
        }
    }

    public void RestartGame()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(0); 
    }
}
