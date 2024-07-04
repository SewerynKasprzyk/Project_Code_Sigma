using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new();
    public int amountToChangeStat;

    public void UseItem()
    {
        if(statToChange == StatToChange.Health)
        { 
            PlayerStats playerStats = GameObject.Find("PlayerJG").GetComponent<PlayerStats>();
            playerStats.playerHp += amountToChangeStat;
        }
        else if(statToChange == StatToChange.Attack)
        {
            PlayerStats playerStats = GameObject.Find("PlayerJG").GetComponent<PlayerStats>();
            playerStats.attackDamage += amountToChangeStat;
        }
        else if(statToChange == StatToChange.Speed)
        {
            PlayerStats playerStats = GameObject.Find("PlayerJG").GetComponent<PlayerStats>();
            playerStats.playerMovementSpeed += amountToChangeStat;
        }
    }

    public enum StatToChange
    {
        None,
        Health,
        Attack,
        Speed
    }
}
