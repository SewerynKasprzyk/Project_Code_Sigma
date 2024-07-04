using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponSO : ScriptableObject
{
    public string itemName;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;

    public void EquipItem()
    {
        Debug.Log("Equipping item");
        PlayerStats playerStats = GameObject.Find("PlayerJG").GetComponent<PlayerStats>();
        playerStats.attackDamage += attackDamage;
        playerStats.attackSpeed += attackSpeed;
        playerStats.attackRange += attackRange;
    }

    public void UnequipItem()
    {
        PlayerStats playerStats = GameObject.Find("PlayerJG").GetComponent<PlayerStats>();
        playerStats.attackDamage -= attackDamage;
        playerStats.attackSpeed -= attackSpeed;
        playerStats.attackRange -= attackRange;
    }


}
