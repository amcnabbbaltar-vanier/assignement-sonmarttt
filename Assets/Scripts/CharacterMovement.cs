using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
     // ============================== Movement Settings ==============================
    [Header("Movement Settings")]
    [SerializeField] private float baseWalkSpeed = 5f;    // Base speed when walking
    [SerializeField] private float baseRunSpeed = 8f;     // Base speed when running


    // ============================== Jump Settings =================================
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;        // Jump force applied to the character
    [SerializeField] private float groundCheckDistance = 1.1f; // Distance to check for ground contact (Raycast)


    [Header("Double Jump")]
    public bool canDoubleJump = false;
    public bool hasDoubleJump = false;


    // ============================== Modifiable from other scripts ==================
    public float speedMultiplier = 1.0f; // Additional multiplier for character speed ( WINK WINK )

    // ============================== Private Variables ==============================
    private Rigidbody rb; // Reference to the Rigidbody component
    private Transform cameraTransform; // Reference to the camera's transform

    // Input variables
    private float moveX; // Stores horizontal movement input (A/D or Left/Right Arrow)
    private float moveZ; // Stores vertical movement input (W/S or Up/Down Arrow)
    private bool jumpRequest; // Flag to check if the player requested a jump
    private Vector3 moveDirection; // Stores the calculated movement direction

    // ============================== Animation Variables ==============================
    [Header("Anim values")]
    public float groundSpeed; // Speed value used for animations

    // ============================== Character State Properties ==============================
    /// <summary>
    /// Checks if the character is currently grounded using a Raycast.
    /// If false, the character is in the air.
    /// </summary>
    public bool IsGrounded => 
        Physics.Raycast(transform.position + Vector3.up * 0.01f, Vector3.down, groundCheckDistance);

    /// <summary>
    /// Checks if the player is currently holding the "Run" button.
    /// </summary>
    private bool IsRunning => Input.GetButton("Run");

    // ============================== Unity Built-in Methods ==============================

    /// <summary>
    /// Called when the script is first initialized.
    /// </summary>
    private void Awake()
    {
        InitializeComponents(); // Initialize Rigidbody and Camera reference
    }

    /// <summary>
    /// Called every frame, used to register player input.
    /// </summary>
    private void Update()
    {
        RegisterInput(); // Collect player input
        if (IsGrounded)
            hasDoubleJump = false;
    }

    /// <summary>
    /// Called every physics update (FixedUpdate ensures physics stability).
    /// </summary>
    private void FixedUpdate()
    {
        HandleMovement(); // Process movement and physics-based updates
    }

    // ============================== Initialization ==============================

    /// <summary>
    /// Initializes Rigidbody and camera reference.
    /// Also locks and hides the cursor for better control.
    /// </summary>
    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        rb.freezeRotation = true; // Prevent Rigidbody from rotating due to physics interactions
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Smooth physics interpolation

        // Assign the main camera if available
        if (Camera.main)
            cameraTransform = Camera.main.transform;

        // Lock and hide the cursor for better gameplay control
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // ============================== Input Handling ==============================

    /// <summary>
    /// Reads player input values and registers movement/jump requests.
    /// </summary>
    private void RegisterInput()
    {
        moveX = Input.GetAxis("Horizontal"); // Get horizontal movement input
        moveZ = Input.GetAxis("Vertical");   // Get vertical movement input

        
    }

    // ============================== Movement Handling ==============================

    /// <summary>
    /// Handles movement-related logic: calculating direction, jumping, rotating, and moving.
    /// </summary>
    private void HandleMovement()
    {
        CalculateMoveDirection(); // Compute the movement direction based on input
        HandleJump(); // Process jump input
        RotateCharacter(); // Rotate the character towards the movement direction
        MoveCharacter(); // Move the character using velocity-based movement
    }

    /// <summary>
    /// Calculates the movement direction on x axis
    /// </summary>
    private void CalculateMoveDirection()
    {
        moveDirection = new Vector3(moveX, 0f, 0f).normalized;
    }

    /// <summary>
    /// Handles jumping by applying an impulse force if the character is grounded.
    /// </summary>
    private void HandleJump()
    {
        // Apply jump force only if jump was requested and the character is grounded
        if (jumpRequest && IsGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply force upwards
            jumpRequest = false; // Reset jump request after applying jump
        }
    }

    /// <summary>
    /// Rotates the character towards the movement direction. only focus on X axis
    /// </summary>
    private void RotateCharacter()
{
    if (moveX > 0)
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);  // facing right
    else if (moveX < 0)
        transform.rotation = Quaternion.Euler(0f, -90f, 0f); // facing left
}

    /// <summary>
    /// Moves the character by setting the Rigidbody's velocity based on the input direction and speed.
    /// </summary>
    private void MoveCharacter()
{
    float speed = IsRunning ? baseRunSpeed : baseWalkSpeed;
    groundSpeed = (moveDirection != Vector3.zero) ? speed : 0.0f;

    rb.velocity = new Vector3(
        moveDirection.x * speed * speedMultiplier,
        rb.velocity.y,
        0f  // lock Z axis
    );
}


    public void Jump(float force)
    {
        if (IsGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
        else if (canDoubleJump && !hasDoubleJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            hasDoubleJump = true;
        }
    }

    public IEnumerator EnableDoubleJump(float duration)
    {
        canDoubleJump = true;
        yield return new WaitForSeconds(duration);
        canDoubleJump = false;
    }

    public IEnumerator EnableSpeedBoost(float duration)
    {
        speedMultiplier = 1.4f; 
        yield return new WaitForSeconds(duration);
        speedMultiplier = 1f; 
    }
}
