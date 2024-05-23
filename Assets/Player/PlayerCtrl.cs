using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator anim;
    private Collider2D col;

    //Dashing variables
    public float dashDuration;
    public float dashSpeedMultiplier;
    private bool isDashing;
    private float dashEndTime;

    private bool faceLeft = false;

    private float movSpeed;
    private PlayerStats playerStats;

    //dodac po tagu sciane i w razie kolizji ze scnia col enable = true
    //animacja atkaowania dopiero po skonczeniu animacji chodzniea

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        col = GetComponent<Collider2D>();

        if (playerStats != null)
        {
            movSpeed = playerStats.playerMovementSpeed;
        }
        else
        {
            Debug.Log("Player component not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerStats = GetComponent<PlayerStats>();
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(movX, movY).normalized;

        //Walking + animation 
        if (movX != 0 || movY != 0)
        {
            anim.SetBool("isWalking", true);
        }

        //Idle animation
        if (movX == 0 && movY == 0)
        {
            anim.SetBool("isWalking", false);
        }

        //Attacking animation
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
        }

        //Dashing animation
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && (movX != 0 || movY != 0))
        {
            isDashing = true;
            dashEndTime = Time.time + dashDuration;
            anim.SetBool("isDashing", true);
        }
        if (isDashing)
        {
            rb.velocity = movement * movSpeed * dashSpeedMultiplier;

            if(col != null)
            {
                col.enabled = false;
            }
            
            if (Time.time > dashEndTime)
            {
                isDashing = false;
                anim.SetBool("isDashing", false);
                col.enabled = true;
            }
            
        }
        else
        {
            rb.velocity = movement * movSpeed;
        }

        RotateTowardsCursor();
    }

    private void RotateTowardsCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;

        if (direction.x < 0 && !faceLeft)
        {
            Flip();
            faceLeft = true;
        }
        else if (direction.x > 0 && faceLeft)
        {
            Flip();
            faceLeft = false;
        }
    }
    public void EndAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    public void EndDash()
    {
        anim.SetBool("isDashing", false);
    }

    private void Flip()
    {
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
