using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // rigidbody
    Rigidbody2D rb;

    // fixed delta time
    float dt;

    // isGrounded
    bool isGrounded;
    [SerializeField] Transform groundCheckPosition;
    [SerializeField] Vector2 groundCheckBoxSize;
    [SerializeField] LayerMask groundLayerMask;

    // jump
    int jumpsAvailable = 1;
    bool jumpDetection;
    bool jumpInput;
    [SerializeField] float jumpForce = 10f;

    // coyote
    [SerializeField] float coyoteTime = 0.5f;
    float coyoteTimeCounter;




    void Start()
    {
        dt = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //
        jumpInput = Input.GetKey(KeyCode.Space);
        // jump input
        JumpInput();
        // check if grounded
        IsGrounded();

        // debug
        Debug.Log("IsGrounded: " + isGrounded + " | jumpDetection: " + jumpDetection + " | jumpInput: " + jumpInput + " | jumpsAvailable: " + jumpsAvailable);
    }

    void FixedUpdate()
    {
        Jump();
    }


    void IsGrounded()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPosition.position, groundCheckBoxSize, 0f, groundLayerMask);
    }

    void JumpInput()
    {
        jumpDetection = Input.GetKeyDown(KeyCode.Space);
        jumpInput = Input.GetKey(KeyCode.Space);
    }


    void Jump()
    {
        //if (isGrounded && jumpInput)
        if (isGrounded && jumpInput)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /*
    void CoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= dt;
        }
    }
    */

}
