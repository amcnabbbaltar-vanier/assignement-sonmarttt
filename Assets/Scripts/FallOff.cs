using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallOff : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger hit by: " + other.name + " | Tag: " + other.tag);
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
