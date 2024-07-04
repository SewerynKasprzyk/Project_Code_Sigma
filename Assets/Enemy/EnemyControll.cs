using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    public float speed;
    public float distanceBetween; //Dystans po jakim enemy zacznie sie poruszac w strone gracza
    public float randomMoveDuration; //Czas po jakim enemy zmieni kierunek ruchu
    public float attackAnimationDuration; //Czas trwania animacji ataku na ktora enemy nie moze sie ruszac

    private float distance;
    private bool isStunned = false;
    private bool isRandomMoving = false;

    private EnemyStats enemyStats;
    private PolygonCollider2D attackCollider;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        enemyStats = GetComponent<EnemyStats>();

        //Atack area colider
        attackCollider = GetComponentInChildren<PolygonCollider2D>();
        attackCollider.enabled = false;

        //Losowe poruszanie sie enemy
        StartCoroutine(IdleAndRandomMovement());
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if(distanceToPlayer <= enemyStats.enemyVisionRange)
            {
                AIChase();
                // anim.SetBool("isRunning", true);
            }
            else
            {
                if (!isRandomMoving)
                {
                    StartCoroutine(IdleAndRandomMovement());
                }                
            }
           

            if (distanceToPlayer <= enemyStats.enemyAttackRange)
            {
                StartAttackAnimation();

            }            
        }
    }

    private void AIChase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position);
        direction.Normalize();

        if (distance < distanceBetween)
        {
            StopCoroutine(IdleAndRandomMovement());
            isRandomMoving = false;

            anim.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();

            // Flip the sprite depending on the player's position
            if (player.transform.position.x < transform.position.x)
            {
                // Player is on the left side, flip the sprite
                spriteRenderer.flipX = true;

                boxCollider.offset = new Vector2(-boxCollider.offset.x, boxCollider.offset.y);

                Vector2[] points = polygonCollider.points;
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].x = -points[i].x;
                }
                polygonCollider.points = points;

            }
            else
            {
                // Player is on the right side, don't flip the sprite
                spriteRenderer.flipX = false;

                boxCollider.offset = new Vector2(Mathf.Abs(boxCollider.offset.x), boxCollider.offset.y);

                Vector2[] points = polygonCollider.points;
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].x = Mathf.Abs(points[i].x);
                }
                polygonCollider.points = points;
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

    private IEnumerator IdleAndRandomMovement()
    {
        while (true)
        {
            if (!isRandomMoving)
            {
                isRandomMoving = true;

                // Losowe opóŸnienie przed ruchem
                float idleDuration = Random.Range(1f, 3f);
                yield return new WaitForSeconds(idleDuration);

                anim.SetBool("isIdle", true); // Ustaw animacjê idle

                // Losowy ruch
                Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                float moveDuration = Random.Range(1f, randomMoveDuration);

                for (float t = 0; t < moveDuration; t += Time.deltaTime)
                {
                    transform.position += (Vector3)(randomDirection * speed * Time.deltaTime);
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isIdle", false); // Wy³¹cz animacjê idle

                    if (randomDirection.x < 0)
                    {
                        spriteRenderer.flipX = true;
                    }
                    else
                    {
                        spriteRenderer.flipX = false;
                    }

                    yield return null;
                }

                anim.SetBool("isWalking", false);
                isRandomMoving = false;
            }

            yield return null;
        }
    }

    private void EndWalking()
    {
        anim.SetBool("isWalking", false);
    }

    private void EndIdle()
    {
        anim.SetBool("isIdle", false);
    }
    private void EndDeath()
    {
        anim.SetBool("isDead", false);
    }

    private void EndAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    private void EndRunning()
    {
        anim.SetBool("isRunning", false);
    }

    private void StartAttackAnimation()
    {
        anim.SetBool("isAttacking", true);
    }

    private void StartAtackHitbox()
    {     
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
        }
    }

    private void EndAttackHitbox()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }
    
}
