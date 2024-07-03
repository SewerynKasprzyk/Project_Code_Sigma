using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // Start is called before the first frame update

    public float enemyHp;
    public float enemyDamage;
    public float stunDuration = 0.2f;

    private Animator anim;
    private Rigidbody2D enemy;
    private EnemyControll enemyControll;

    void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<Rigidbody2D>();
        enemyControll = GetComponent<EnemyControll>();

        if (enemy == null)
        {
            Debug.LogError("Rigidbody2D component not found on enemy object");
        }
        if (enemyControll == null)
        {
            Debug.LogError("EnemyControll component not found on enemy object");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage, Vector2 knockbackDirection, float knockbackForce)
    {
        enemyHp -= damage;

        if (enemyHp <= 0)
        {
            anim.SetBool("isDead", true);
            enemyControll.speed = 0;
        }
        else
        {
            PlayHurtAnimation();
            ApplyKnockback(knockbackDirection, knockbackForce, stunDuration);
            StartCoroutine(StunEnemy());
        }
    }

    public void DealDamage()
    {

    }



    public IEnumerator ApplyKnockback(Vector2 knockbackDirection, float knockbackForce, float knockbackDuration)
    {
        float timer = 0f;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + knockbackDirection * knockbackForce;

        while (timer < knockbackDuration)
        {
            timer += Time.deltaTime;
            float t = timer / knockbackDuration;
            enemy.MovePosition(Vector2.Lerp(startPosition, targetPosition, t));
            yield return null;
        }
    }
    
    public void Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private void PlayHurtAnimation()
    {
        //anim.SetBool("isWalking", false);
        anim.SetBool("isHurt", false);
        anim.SetBool("isHurt", true);
    }

    private void EndHurt()
    {
        anim.SetBool("isHurt", false);
    }

    private IEnumerator StunEnemy()
    {
        if (enemyControll != null)
        {
            enemyControll.Stun(stunDuration);
        }
        yield return new WaitForSeconds(stunDuration);
    }

}
