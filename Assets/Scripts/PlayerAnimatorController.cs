using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatiorController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    private Rigidbody rb;

    private float chargeTime;
    public float maxChargeTime = 3.0f;
    public bool isHoldingJump = false;
    public float minJumpForce = 6.0f;
    

    public void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
         rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        animator.SetFloat("CharacterSpeed", rb.velocity.magnitude);
        animator.SetBool("IsGrounded", movement.IsGrounded);
        animator.SetFloat("VerticalVelocity", rb.velocity.y);

        // Handle jump charging
        if (Input.GetKey(KeyCode.Space))
        {
            if(!isHoldingJump) isHoldingJump = true;
                chargeTime += Time.deltaTime;
                chargeTime = Mathf.Clamp(chargeTime, 0.0f, maxChargeTime);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isHoldingJump)
        {
                float jumpForce = minJumpForce + (chargeTime / maxChargeTime);
                movement.Jump(jumpForce);
                chargeTime = 0.0f; 
                isHoldingJump = false;
        }
    }
}





