using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public ItemSlot[] itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(string itemName, Sprite itemSprite)
    {
        Debug.Log("Added " + itemName + " to inventory");


        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, itemSprite);
                itemSlot[i].isFull = true;
                return;
            }
        }
    }

    public void ReplaceItem(int slotIndex, Item item)
    {

        itemSlot[slotIndex].AddItem(item.GetItemName(), item.GetItemSprite());
        itemSlot[slotIndex].isFull = true;
        Debug.Log("Replaced item in slot " + slotIndex);
    }

    public bool IsInventoryFull()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                return false;
            }
        }
        return true;
    }



 
}
