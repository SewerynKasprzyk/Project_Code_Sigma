using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class WeaponSO : ScriptableObject
{
    public string itemName;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;

    public void EquipItem()
    {
        //Debug.Log("Equipping item");
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

    public void DialogSet(Sprite itemsprite)
    {
        DialogStats dialogStats = GameObject.Find("DialogWindowPanel").GetComponent<DialogStats>();
        Debug.Log("przed zmiana nazwy");
        dialogStats.Name.text = itemName;
        Debug.Log("zmiana nazwy");
        dialogStats.Damage.SetText(attackDamage.ToString());
        dialogStats.Speed.SetText(attackSpeed.ToString());
        dialogStats.Range.SetText(attackRange.ToString());
        dialogStats.itemImage.sprite = itemsprite;
    }

    public void DialogClear()
    {
        //itemName = null;
        //DialogStats dialogStats = GameObject.Find("DialogWindowPanel").GetComponent<DialogStats>();
        //dialogStats.Damage.SetText("");
        //dialogStats.Speed.SetText("");
        //dialogStats.Range.SetText("");
        //dialogStats.itemImage.sprite = null;
    }
}
