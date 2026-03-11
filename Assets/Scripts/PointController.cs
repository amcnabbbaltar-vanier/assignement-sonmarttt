using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public enum PickupType { Speed, Jump, Score }
    public PickupType pickupType;

    public float rotateSpeed = 90f;
    public float hoverHeight = 0.2f;
    public float hoverSpeed = 2f;
    public float hover;
    private Vector3 startPosition;

    public GameObject collectParticle;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        hover = Random.Range(0f, 2f * Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed + hover) * hoverHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    // Handle point collision with player, make is disappear and apply the effect
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        if (collectParticle != null)
        {
            GameObject effect = Instantiate(collectParticle, transform.position, Quaternion.identity);
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();
            ps.Play(); 
            Destroy(effect, 2f);
        }
        CharacterMovement movement = other.GetComponent<CharacterMovement>();

        switch (pickupType)
        {
            case PickupType.Jump:
                movement.StartCoroutine(movement.EnableDoubleJump(30f));
                break;
            case PickupType.Score:
                GameManager.Instance.AddScore(50);
                break;
            case PickupType.Speed:
                movement.StartCoroutine(movement.EnableSpeedBoost(5f));
                break;
        }


        gameObject.SetActive(false);
    }
}
}
