using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool canMove = false;
    public MoveDirection moveDirection;
    private bool isMoving = false;
    public enum MoveDirection { LeftRight, UpDown }
    public float moveDistance = 2f;
    public float moveSpeed = 2f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(StartMovingAfterDelay());
    }

    IEnumerator StartMovingAfterDelay()
    {
        yield return new WaitForSeconds(Random.Range(0f, 3f)); // random delay between 0 and 3 seconds
        isMoving = true;
    }


    void Update()
    {
        if (canMove && isMoving)
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
        Debug.Log("Trigger hit by: " + other.name + " | Tag: " + other.tag);
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage();
        }
    }
}
