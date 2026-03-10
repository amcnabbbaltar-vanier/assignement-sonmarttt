using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachGoal : MonoBehaviour
{
    public float hoverHeight = 0.2f;
    public float hoverSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // load next level
            int nextScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
    }
}
