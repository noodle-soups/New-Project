using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;

    float inputHorizontal;
    float inputVertical;
    [SerializeField] float moveSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(inputHorizontal * moveSpeed, inputVertical * moveSpeed);     

        Debug.Log(inputHorizontal);        
        Debug.Log(inputVertical);        
    }
}
