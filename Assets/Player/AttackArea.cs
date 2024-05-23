using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;//damage bedzie brany z broni to do testa
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyStats>() != null)
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(damage);
        }
    }
}
