using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public enum PickupType { Speed, Jump, Score }
    public PickupType pickupType;

    public float rotateSpeed = 90f;

    public GameObject collectParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
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
                GameManager.Instance.AddScore(40);
                break;
            case PickupType.Speed:
                movement.StartCoroutine(movement.EnableSpeedBoost(5f));
                break;
        }


        gameObject.SetActive(false);
    }
}
}
