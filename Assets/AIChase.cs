using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player; 
    public float speed;
    public float distanceBetween;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position);
        direction.Normalize();

        

        if (distance < distanceBetween)
        {
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
    }
}
