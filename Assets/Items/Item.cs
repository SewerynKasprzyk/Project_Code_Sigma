using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    private static int nextItemID = 0;
    private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private Sprite itemIcon;

    protected Item()
    {
        itemID = nextItemID;
        nextItemID++;
    }

    public virtual void Use()
    {
        // Use the item
    }
}