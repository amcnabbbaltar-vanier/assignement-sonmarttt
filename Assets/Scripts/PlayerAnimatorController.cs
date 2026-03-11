using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatiorController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    private Rigidbody rb;
    private AudioSource audioSource;
    public AudioClip doubleJumpSound;

    private float chargeTime;
    public float maxChargeTime = 3.0f;
    public bool isHoldingJump = false;
    public float minJumpForce ;
    public float maxJumpForce;
    

    public void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
{
    if (Input.GetKey(KeyCode.Space) && isHoldingJump)
    {
        chargeTime += Time.deltaTime;
        chargeTime = Mathf.Min(chargeTime, 3f);
    }
}

    private void Update()
    {
        animator.SetFloat("CharacterSpeed", rb.velocity.magnitude);
        animator.SetBool("IsGrounded", movement.IsGrounded);
        animator.SetFloat("VerticalVelocity", rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            chargeTime = 0f;
            isHoldingJump = true;
            Debug.Log("Started charging at: " + Time.time);
        }

        

        if (Input.GetKeyUp(KeyCode.Space) && isHoldingJump)
        {
            float charge = chargeTime / 3f; // 0.0 to 1.0, capped
            float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, charge);
            Debug.Log("Charge: " + charge + " | Force: " + jumpForce);

            if (!movement.IsGrounded && movement.canDoubleJump){
                animator.SetTrigger("doDouble");
                audioSource.PlayOneShot(doubleJumpSound);
            }
            else
                animator.SetTrigger("Jump");

            movement.Jump(jumpForce);
            chargeTime = 0f;
            isHoldingJump = false;
        }
    }
}





