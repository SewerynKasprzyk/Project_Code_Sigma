using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new();
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new();
    public int amountToChangeAttribute;

    public void UseItem()
    {
        if(statToChange == StatToChange.Health)
        { 
            //GameObject.Find("HealthManager").GetComponent<PlayerHealth>().ChangeHealth(amountToChangeStat);
        }
    }




    public enum StatToChange
    {
        None,
        Health,
        Attack,
        Defense,
        Speed
    }

    public enum ItemType
    {
        None,
        Consumable,
        Weapon,
        Armor,
        Accessory
    }

    public enum ItemTier
    {
        None,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public enum AttributeToChange
    {
        None,
        Health,
        Attack,
        Defense,
        Speed
    }

}
