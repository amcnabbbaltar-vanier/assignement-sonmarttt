using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartScore : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnLevelStart();
    }
}
