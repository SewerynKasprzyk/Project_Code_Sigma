using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    public float movSpeed; 
    public Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        if(Mathf.Abs(movX) > 0 )
        {
            rb.velocity = new Vector2(movX * movSpeed, rb.velocity.y);
        }
        if (Mathf.Abs(movY) > 0 )
        {
            rb.velocity = new Vector2(rb.velocity.x, movY * movSpeed);
        }
    }
}
