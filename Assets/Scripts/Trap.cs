using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool canMove = false;
    public enum MoveDirection { LeftRight, UpDown }
    public MoveDirection moveDirection;
    public float moveDistance = 2f;
    public float moveSpeed = 2f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    

    void Update()
    {
        if (canMove)
        {
            float moveAmount = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
            if (moveDirection == MoveDirection.LeftRight)
            {
                transform.position = new Vector3(startPosition.x + moveAmount, startPosition.y, startPosition.z);
            }
            else
            {
                transform.position = new Vector3(startPosition.x, startPosition.y + moveAmount, startPosition.z);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage();
        }
    }
}
