using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Magic Weapon", menuName = "Items/Melee")]
public class Magic : Item
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;

    public override void Use()
    {
        base.Use();
        // Use the magic weapon
    }
}
