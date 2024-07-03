using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Items/Melee")]
public class Melee : Item
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;

    public override void Use()
    {
        base.Use();
        // Use the melee weapon
    }
}
