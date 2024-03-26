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

    void Start()
    {
        dt = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // check if grounded
        IsGrounded();

        // debug
        Debug.Log("IsGrounded: " + isGrounded);
    }

    void IsGrounded()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPosition.position, groundCheckBoxSize, 0f, groundLayerMask);
    }


}
