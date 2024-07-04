using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject utilityPanel;
    public ItemSlot[] itemSlot;
    public UtilitySlot[] utilitySlot;

    private int lastThisItemSelected = -1;
    private WeaponSOLib weaponSOLib;

    // Start is called before the first frame update
    void Start()
    {
        weaponSOLib = GameObject.Find("InventoryCanvas").GetComponent<WeaponSOLib>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            KeyCode key = KeyCode.None;
            if (Input.GetKeyDown(KeyCode.Alpha1)) key = KeyCode.Alpha1;
            else if (Input.GetKeyDown(KeyCode.Alpha2)) key = KeyCode.Alpha2;
            else if (Input.GetKeyDown(KeyCode.Alpha3)) key = KeyCode.Alpha3;

            switch (key)
            {
                case KeyCode.Alpha1:
                    Debug.Log("Pressed 1");
                    ActivateSlot(0);
                    break;
                case KeyCode.Alpha2:
                    Debug.Log("Pressed 2");
                    ActivateSlot(1);
                    break;
                case KeyCode.Alpha3:
                    Debug.Log("Pressed 3");
                    ActivateSlot(2);
                    break;
                default:
                    // Opcjonalnie: obs³uga innych klawiszy lub brak akcji
                    break;
            }
        }
    }

    public void ActivateSlot(int index)
    {
        if (index >= 0 && index < itemSlot.Length)
        {
            DeselectAllItems(); // Odznacz wszystkie sloty

            for (int i = 0; i < itemSlot.Length; i++)
            {
                for (int j = 0; j < weaponSOLib.weaponSO.Length; j++)
                {
                    if (weaponSOLib.weaponSO[j].itemName == itemSlot[i].itemName && i == lastThisItemSelected)
                        weaponSOLib.weaponSO[j].UnequipItem();
                }
            }

            for (int i = 0; i < weaponSOLib.weaponSO.Length; i++)
            {
                //Debug.Log("Checking " + weaponSOLib.weaponSO[i].itemName);
                if (weaponSOLib.weaponSO[i].itemName == itemSlot[index].itemName)
                {
                    //Debug.Log("found " + weaponSOLib.weaponSO[i].itemName);
                    weaponSOLib.weaponSO[i].EquipItem();
                    Debug.Log("ubrano " + weaponSOLib.weaponSO[i].itemName);
                }
            }

            itemSlot[index].SelectedShader.SetActive(true); // Aktywuj shader dla wybranego slotu
            itemSlot[index].thisItemSelected = true; // Oznacz slot jako wybrany
            lastThisItemSelected = index; // Zapamiêtaj ostatni wybrany slot
        }
    }

/*    public void UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                itemSOs[i].UseItem();
                return;
            }
        }   
    }*/

    public void AddItem(string itemName, Sprite itemSprite, ItemType itemType)
    {
        if (itemType == ItemType.Weapon)
        {
            Debug.Log("Added " + itemName + " to inventory");

            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull == false)
                {
                    itemSlot[i].AddItem(itemName, itemSprite, itemType);
                    return;
                }
            }
        }
        else 
        {
            Debug.Log("Added " + itemName + " to inventory");

            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (utilitySlot[i].isFull == false)
                {
                    utilitySlot[i].AddItem(itemName, itemSprite, itemType);
                    return;
                }
            }
        }
    }

/*    public void ReplaceItem(int slotIndex, Item item)
    {

        itemSlot[slotIndex].AddItem(item.GetItemName(), item.GetItemSprite());
        itemSlot[slotIndex].isFull = true;
        Debug.Log("Replaced item in slot " + slotIndex);
    }*/

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
    public void DeselectAllItems()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].SelectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}

public enum ItemType
{
    None,
    Consumable,
    Weapon,
};