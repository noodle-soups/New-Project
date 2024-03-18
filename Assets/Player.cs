using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;

    // movePlayer
    float inputHorizontal;
    [SerializeField] float moveSpeedOld = 10f;

    float moveInput;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float acceleration = 7f;
    [SerializeField] float decceleration = 7f;
    [SerializeField] float velPower = 0.9f;
 


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

 
    void Update()
    {
        //movePlayer();
        newMovePlayer();
    }

    void movePlayer()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeedOld;
        rb.AddForce(inputHorizontal * Vector2.right);     
    }

    void newMovePlayer()
    {
        // 
        moveInput = Input.GetAxisRaw("Horizontal");
        float targetSpeed = moveInput * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        //rb.AddForce(targetSpeed * Vector2.right);
        rb.AddForce(movement * Vector2.right);
    }

}
