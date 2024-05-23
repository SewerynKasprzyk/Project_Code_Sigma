using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;//damage bedzie brany z broni to do testa
    private float knockbackForce = 8f;
    private float knockbackDuration = 1.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyStats>() != null)
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            Debug.Log("Knockback Direction: " + knockbackDirection); // Debug

            // Tutaj zaczynamy interpolacjê knockbacku
            StartCoroutine(enemyStats.ApplyKnockback(knockbackDirection, knockbackForce, knockbackDuration));
           

            enemyStats.TakeDamage(damage, knockbackDirection, knockbackForce);
        }
    }
}
