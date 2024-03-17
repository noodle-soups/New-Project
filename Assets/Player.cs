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
        inputHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        inputVertical = Input.GetAxisRaw("Vertical") * moveSpeed;
        rb.velocity = new Vector2(inputHorizontal, inputVertical);     

        /*
        inputHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        inputVertical = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(inputHorizontal, inputVertical, 0f);
        */

        Debug.Log(inputHorizontal);        
        Debug.Log(inputVertical);        
    }
}
