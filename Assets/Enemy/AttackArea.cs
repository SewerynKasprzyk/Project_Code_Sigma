using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Enemy
{
    internal class AttackArea : MonoBehaviour
    {
        private float damage;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerStats>() != null)
            {
                EnemyStats enemyStats = GetComponentInParent<EnemyStats>();
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();

                damage = enemyStats.enemyWeaponDamage;

                playerStats.TakeDamage(damage);
            }
        }
    }
}
