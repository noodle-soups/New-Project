using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb; // player rigidbody
    float dt; // Time.deltaTime

    // player movement
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



 


    void Start()
    {
        dt = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody2D>();   
    }

 
    void Update()
    {
        GroundedCheck();
    }


    void FixedUpdate()
    {
        movePlayer();
        playerJump();
    }


    void movePlayer()
    {
        // get player input
        moveInput = Input.GetAxisRaw("Horizontal");
        // compute our direction & desired velocity
        float targetSpeed = moveInput * moveSpeed;
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

    void GroundedCheck()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPosition.position, groundCheckBoxSize, 0f, groundLayerMask);
    }

    void playerJump()
    {
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        if (jumpPressed)
        {
            Debug.Log(jumpPressed);
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }
    }

}
