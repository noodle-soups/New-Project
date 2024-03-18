using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;

    float inputHorizontal;
    [SerializeField] float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    void movePlayer()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        rb.AddForce(inputHorizontal * Vector2.right);     
    }

}
