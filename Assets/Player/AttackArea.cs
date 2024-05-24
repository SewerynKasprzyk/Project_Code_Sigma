using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;//damage bedzie brany z broni to do testa
    private float knockbackForce = 1.5f;
    private float knockbackDuration = 0.2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyStats>() != null)
        {

            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            Debug.Log("Knockback Direction: " + knockbackDirection); // Debug

            enemyStats.TakeDamage(damage, knockbackDirection, knockbackForce);

            // Tutaj zaczynamy interpolacjê knockbacku
            // !!!!! UWAGA !!!!! nie robimy tak poniewa¿ to jest z³y sposób na startowanie coroutine z innego skryptu, poniewa¿ usuwaj¹c obiekt coroutine nie zostanie zniszczona i wywo³a b³¹d.
            //StartCoroutine(enemyStats.ApplyKnockback(knockbackDirection, knockbackForce, knockbackDuration));
            // Zamiast tego robimy tak:
            enemyStats.StartCoroutine(enemyStats.ApplyKnockback(knockbackDirection, knockbackForce, knockbackDuration));
        }
    }
}
