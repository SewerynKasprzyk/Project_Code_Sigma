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
    private bool isPanelActive = false;

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
            if (!InventoryManager.IsInventoryWeaponFull() && itemType == ItemType.Weapon)
            {
                InventoryManager.AddWeaponItem(itemName, itemSprite, itemType);
                Destroy(gameObject);
                Debug.Log("kurwa");
            }
            else
            {
                InventoryManager.ShowInventoryFullDialog(itemName, itemSprite, itemType);
                isPanelActive = true;
            }
            if (!InventoryManager.IsInventoryUtilityFull() && itemType == ItemType.Consumable)
            {
                InventoryManager.AddUtilityItem(itemName, itemSprite, itemType);
                Destroy(gameObject);
                Debug.Log("taco");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isPanelActive)
            {
                InventoryManager.HideInventoryFullDialog(itemName);
                isPanelActive = false;
            }
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