using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // rigidbody
    Rigidbody2D rb;

    // fixed delta time
    float dt;

    // player movement
    float moveInput;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float decceleration = 10f;
    [SerializeField] float velPower = 0.9f;

    // animation
    SpriteRenderer spriteRenderer;


    void Start()
    {
        dt = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        // Check for movement input
        moveInput = Input.GetAxisRaw("Horizontal");

        // debug
        //Debug.Log("Testing...");
    }

    void FixedUpdate()
    {
        MovePlayer();
        FlipSprite();
    }

    void MovePlayer()
    {
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

    void FlipSprite()
    {
        if (rb.velocity.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        if (rb.velocity.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }

}
