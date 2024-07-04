using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private static int nextItemID = 0;
    private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private Sprite itemSprite;
    public ItemType itemType;
    [SerializeField] private int tier;
    private InventoryManager InventoryManager;

    private void Start()
    {
        InventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        //itemCollisonHandler = GameObject.Find("ItemPIckUpCanvas").GetComponent<ItemCollisonHandler>();
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!InventoryManager.IsInventoryFull())
            {
                InventoryManager.AddItem(itemName, itemSprite, itemType);
                Destroy(gameObject);

            }
            //else
            //itemCollisonHandler.ShowPickUpDialog(this);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //itemCollisonHandler.HidePickUpDialog();
        }
    }

    protected Item()
    {
        itemID = nextItemID;
        nextItemID++;
    }

    public virtual void Use()
    {
        // Use the item
    }

    public string GetItemName()
    {
        return itemName;
    }

    internal Sprite GetItemSprite()
    {
        return itemSprite;
    }
}