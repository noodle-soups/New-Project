using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    float dt;

    // player movement
    bool isWalking;
    float moveInput;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float acceleration = 7f;
    [SerializeField] float decceleration = 7f;
    [SerializeField] float velPower = 0.9f;

    // isGrounded
    private bool isGrounded;
    public Transform groundCheckPosition;
    public Vector2 groundCheckBoxSize;
    public LayerMask groundLayerMask;

    // jump
    bool jumpInput;
    bool canJump = true;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float jumpCutMultiplier = 2f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float maxJumpVelocity = 10f;

    // animator
    Animator anim;


    void Start()
    {
        dt = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();   
    }

    void Update()
    {
        // Check for movement input
        moveInput = Input.GetAxisRaw("Horizontal");
        // check for jump input
        jumpInput = Input.GetKey(KeyCode.Space);
        // check if grounded
        IsGrounded();
        // check if walking
        IsWalking();

        // debug
        Debug.Log(isWalking);
    }

    void FixedUpdate()
    {
        MovePlayer();
        PlayerJump();
    }

    void PlayerJump()
    {
        // Perform the jump if the space key is held down, the player is grounded, and the player can jump
        if (jumpInput && isGrounded && canJump)
        {
            // apply jump force
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // Disable jumping until the cooldown period is over
            canJump = false;
            // Start the jump cooldown coroutine
            StartCoroutine(JumpCooldown());
        }

        // fall multiplier
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * dt;
        }
        
        // jump cut
        if (rb.velocity.y > 0 && !jumpInput)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpCutMultiplier - 1) * dt;
        }
    }

    IEnumerator JumpCooldown()
    {
        // cooldown
        yield return new WaitForSeconds(0.1f);
        // refresh
        canJump = true; 
    }

    void MovePlayer()
    {
        // compute our direction & desired velocity
        float targetSpeed = moveInput * moveSpeed;

        // If the player is jumping, limit the horizontal speed
        if (!isGrounded)
        {
            // Apply a maximum horizontal velocity when jumping
            targetSpeed = Mathf.Clamp(targetSpeed, -maxJumpVelocity, maxJumpVelocity);
        }
        
        // compute the difference between current velocity and desired velocity
        float speedDif = targetSpeed - rb.velocity.x;
        // decide whether we are accelerating or deccelerating
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        // apply acceration to speed difference and raise to set power so acceleration increases with higher speeds
        // multiply by sign to reapply direction
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        // apply force to rigidbody, multiply by Vector2.right to affect only the x-axis
        rb.AddForce(movement * Vector2.right * dt);
    }

    void IsGrounded()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPosition.position, groundCheckBoxSize, 0f, groundLayerMask);
    }


    void IsWalking()
    {
        isWalking = isGrounded && rb.velocity.x > 0.01f;
        anim.SetBool("isWalking", isWalking);
    }

}
