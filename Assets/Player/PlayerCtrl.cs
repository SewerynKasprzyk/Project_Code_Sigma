using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float movSpeed;
    public Rigidbody2D rb;

    private Animator anim;

    //Jumping variables
    public float jumpDuration;
    public float jumpSpeedMultiplier;
    private bool isJumping;
    private float jumpEndTime;

    private bool faceLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(movX, movY).normalized;

        //Walking + animation 
        if (movX > 0 && faceLeft)
        {
            rb.velocity = new Vector2(movX * movSpeed, rb.velocity.y);
            anim.SetBool("isWalking", true);
            Flip();
            faceLeft = false;
        }
        if (movY > 0 && !faceLeft)
        {
            rb.velocity = new Vector2(rb.velocity.x, movY * movSpeed);
            anim.SetBool("isWalking", true);
            Flip();
            faceLeft = true;
        }

        //Idle animation
        if (movX == 0 && movY == 0)
        {
            anim.SetBool("isWalking", false);
        }


        //Attacking animation
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isAttacking", true);
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            jumpEndTime = Time.time + jumpDuration;
            anim.SetBool("isDashing", true);
        }
        if (isJumping)
        {
            rb.velocity = movement * movSpeed * jumpSpeedMultiplier;
            if (Time.time > jumpEndTime)
            {
                isJumping = false;
                anim.SetBool("isDashing", false);
            }
        }
        else
        {
            rb.velocity = movement * movSpeed;
        }
    }

    public void endAttack()
    { 
        anim.SetBool("isAttacking", false);
    }

    private void Flip()
    {
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
