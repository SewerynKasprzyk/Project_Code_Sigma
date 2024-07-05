using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Enemy
{
    public class BossControll : MonoBehaviour
    {
        private GameObject boss;
        private GameObject player;
        private Animator anim;
        private SpriteRenderer spriteRenderer;

        public float speed;
        public float distanceBetween;
        public float bossVisionRange;
        public float bossAttackRange;

        private bool isRandomMoving = false;
        private PolygonCollider2D attackArea;
        public float randomMoveDuration;
        private bool isAttackAnimation = false;

        void Start()
        {
            boss = GameObject.FindGameObjectWithTag("Boss");
            player = GameObject.FindGameObjectWithTag("Player");
            anim = boss.GetComponent<Animator>();
            spriteRenderer = boss.GetComponent<SpriteRenderer>();
            attackArea = boss.GetComponentInChildren<PolygonCollider2D>();
            BossInit();

            attackArea.enabled = false;

            if (attackArea != null)
            {
                Debug.Log("Attack area found");

            }
        }

        private void Update()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);


            if (distanceToPlayer <= bossVisionRange)
            {
                AIChase();
            }
            else
            {
                if (!isRandomMoving)
                {
                    StartCoroutine(IdleAndRandomMovement());
                }
            }

            if (distanceToPlayer <= bossAttackRange && !isAttackAnimation)
            {
                RandomBossAttack();
                StartCoroutine(EndAttackAnimation());
            }

            if(isAttackAnimation)
            {
                StopCoroutine(EndAttackAnimation());
            }

            Debug.Log("Status " + isAttackAnimation);

        }
        private IEnumerator EndAttackAnimation()
        {
            yield return new WaitForSeconds(2.5f);
            isAttackAnimation = false;
        }

        
             

        public void AIChase()
        {
            float distance = Vector2.Distance(boss.transform.position, player.transform.position);
            Vector2 direction = (player.transform.position - boss.transform.position).normalized;

            

            if (distance < bossVisionRange)
            {
                if (distance < distanceBetween)
                {
                    StopCoroutine(IdleAndRandomMovement());
                    isRandomMoving = false;

                    anim.SetBool("isWalking", true);
                    boss.transform.position = Vector2.MoveTowards(boss.transform.position, player.transform.position, speed * Time.deltaTime);

                    // Flipping the sprite based on player's position
                    
                    if (player.transform.position.x < boss.transform.position.x)
                    {
                        spriteRenderer.flipX = true;
                    }
                    else
                    {
                        spriteRenderer.flipX = false;
                    }
                }
                else
                {
                    anim.SetBool("isWalking", false);
                }
            }
            else
            {
                if (!isRandomMoving)
                {
                    StartCoroutine(IdleAndRandomMovement());
                }
                       
            }
        }

        private IEnumerator IdleAndRandomMovement()
        {
            while (true)
            {
                if (!isRandomMoving)
                {
                    isRandomMoving = true;

                    // Losowe opóźnienie przed ruchem

                    float idleDuration = UnityEngine.Random.Range(1f, 3f);
                    yield return new WaitForSeconds(idleDuration);

                    anim.SetBool("isIdle", true); // Ustaw animację idle

                    // Losowy ruch
                    Vector2 randomDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
                    float moveDuration = UnityEngine.Random.Range(1f, randomMoveDuration);

                    for (float t = 0; t < moveDuration; t += Time.deltaTime)
                    {
                        transform.position += (Vector3)(randomDirection * speed * Time.deltaTime);
                        anim.SetBool("isWalking", true);
                        anim.SetBool("isIdle", false); // Wyłącz animację idle

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

        public void BossInit()
        {
            Debug.Log("Boss init: " + boss.transform.childCount);
            // Disable all child colliders
            for (int i = 0; i < boss.transform.childCount; i++)
            {
                PolygonCollider2D collider = boss.transform.GetChild(i).GetComponent<PolygonCollider2D>();
                if (collider != null)
                {
                    collider.enabled = false;
                }
            }
        }

        public void RandomBossAttack()
        {
            // Randomly select an attack based on the number of animations that are sub-objects of the boss
            isAttackAnimation = true;
            int randomAttack = UnityEngine.Random.Range(0, boss.transform.childCount);

            Debug.Log("Random attack: " + randomAttack);

            // Pobierz obiekt potomny bossa na podstawie losowego indeksu
            GameObject attackObject = boss.transform.GetChild(randomAttack).gameObject;

            //Dostań się do polygon collidera z obiektu ataku
            attackArea = attackObject.GetComponent<PolygonCollider2D>();
            attackArea.enabled = true;
            
            // Sprawdź, czy obiekt posiada komponent Animator        
            if (anim != null)
            {
                // Jeśli obiekt posiada Animator, ustaw animację
                anim.SetTrigger("isAttacking" + randomAttack);
            }
            else
            {
                // Jeśli obiekt nie posiada Animator, poinformuj o tym
                Debug.LogWarning("Animator component not found on " + attackObject.name);
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
            if (attackArea != null)
            {
                attackArea.enabled = true;
            }
        }

        private void EndAttackHitbox()
        {
            if (attackArea != null)
            {
                attackArea.enabled = false;
            }
        }

        private void EndAttack1()
        {
            isAttackAnimation = false;
            anim.SetBool("isAttacking1", false);            

        }

        private void EndAttack2()
        {
            isAttackAnimation = false;
            anim.SetBool("isAttacking2", false);        

        }

        private void EndAttack3()
        {
            isAttackAnimation = false;
            anim.SetBool("isAttacking3", false);            
        }
    }
}
