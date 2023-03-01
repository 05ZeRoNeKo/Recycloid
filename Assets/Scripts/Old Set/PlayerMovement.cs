using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Values")] //to adjust player controls
    public float moveSpeed;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    [Header("Ground Check")] //to adjust drag and speed control, to check before player jumps
    public float playerHeight; //as reference for raycast
    public LayerMask groundLayer; //layermask to check
    public bool isGrounded; //tells whether player is on ground
    public bool canJump = true;

    public float groundDrag; //adjusts drag if player is on ground

    [Header("Object Reference")] //for referencing game objects
    public Transform orientation;
    

    private PlayerControls playerControls; //for referencing new input system
    private Rigidbody rigidBody;

    //floats for movement calculations
    private float moveX;
    private float moveY;
    private Vector3 moveDir; //to manage input for movement direction
    private void Awake() //for initializing components at scene load
    {
        playerControls = new PlayerControls();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Update() //updates per frame, for handling inputs
    {
        //for ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        PlayerInputs();
        SpeedControl();

        //for handling drag
        if (isGrounded)
        {
            rigidBody.drag = groundDrag;
        }
        else
        {
            rigidBody.drag = 0;
        }
    }
    void FixedUpdate() //updates at intervals, for methods with physics calculations
    {
        Move();
    }

    private void PlayerInputs()
    {
        //for movement inputs
        Vector2 move = playerControls.Player.Move.ReadValue<Vector2>();

        moveX = move.x ;
        moveY = move.y ;

        //for jump inputs
        if (playerControls.Player.Jump.IsPressed() && canJump && isGrounded)
        {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void Move()
    {
        //calculation movement direction
        moveDir = orientation.forward * moveY * Time.deltaTime + orientation.right * moveX * Time.deltaTime;

        //for adding for to the player on ground
        if (isGrounded)
        {
            rigidBody.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        //for adding for to the player in air
        else if(!isGrounded){
            rigidBody.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }
    private void Jump()
    {
        //always reset y to jump consistently
        rigidBody.velocity = -new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);

        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        canJump = true;
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);

        //limit velocity under condition
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
        }
    }
}
