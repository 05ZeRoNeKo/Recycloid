using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator player;

    [Header("Movement Values")]
    public float moveSpeed; //ground movement
    public float airMultiplier; //mid-air movement

    public float jumpForce;
    public float jumpCooldown;
    public bool canJump = true;

    [Header("Ground Check")]
    public float playerHeight; //as reference for positioning raycast
    public LayerMask groundLayer;
    public bool isGrounded;
    public float groundDrag; //adjusts drag if player is on ground

    //referrences on Awake
    private PlayerControls playerControls;
    private Rigidbody rigidBody;
    private Transform mainCamera;

    //variables for movement calculations
    private float moveX;
    private float moveY;
    private Vector3 moveDir; //to manage input for movement direction

    //variables for player transform rotation
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity; //for command reference only

    private void Awake() //for initializing components at scene load
    {
        playerControls = new PlayerControls();
        mainCamera = Camera.main.transform;
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
        PlayerRotate();
    }

    private void PlayerInputs()
    {
        //for movement inputs
        Vector2 move = playerControls.Player.Move.ReadValue<Vector2>();

        moveX = move.x;
        moveY = move.y;

        //for jump inputs
        if (playerControls.Player.Jump.IsPressed() && canJump && isGrounded)
        {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(move.x == 0 && move.y == 0)
        {
            player.SetBool("isIdle", true);
        }
        else
        {
            player.SetBool("isIdle", false);
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);

        //limit velocity under condition
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
        }
    }

    private void Move()
    {
        //calculation movement direction
        moveDir = (moveY * Time.deltaTime * mainCamera.forward) + (moveX * Time.deltaTime * mainCamera.right);
        moveDir.y = 0;

        //for adding force to the player on ground
        if (isGrounded)
        {
            rigidBody.AddForce(10f * moveSpeed * moveDir.normalized, ForceMode.Force);
        }

        //for adding for to the player in air
        else if (!isGrounded)
        {
            rigidBody.AddForce(10f * airMultiplier * moveSpeed * moveDir.normalized, ForceMode.Force);
        }
    }
    private void Jump()
    {
        //always reset y to jump consistently
        rigidBody.velocity = -new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);

        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        player.SetBool("jumped", true);
    }
    private void ResetJump()
    {
        canJump = true;
        player.SetBool("jumped", false);
    }
    private void PlayerRotate()
    {
        if (rigidBody.velocity.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(rigidBody.velocity.x, rigidBody.velocity.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }


}
