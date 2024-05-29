using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    public float speed;
    public float distanceBetween;

    private float distance;
    private bool isStunned = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
        {
            AIChase();
        }
    }

    private void AIChase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position);
        direction.Normalize();


        if (distance < distanceBetween)
        {
            anim.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            // Flip the sprite depending on the player's position
            if (player.transform.position.x < transform.position.x)
            {
                // Player is on the left side, flip the sprite
                spriteRenderer.flipX = true;
            }
            else
            {
                // Player is on the right side, don't flip the sprite
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
       
    }

    public void Stun(float stunDuration)
    {
        StartCoroutine(StunCoroutine(stunDuration));
    }

    public IEnumerator StunCoroutine(float stunDuration)
    {
        isStunned = true;
        anim.SetBool("isWalking", false); // Stop walking animation if stunned
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }

    private void EndWalking()
    {
        anim.SetBool("isWalking", false);
    }

    private void EndIdle()
    {
        anim.SetBool("isIdle", false);
    }

}
