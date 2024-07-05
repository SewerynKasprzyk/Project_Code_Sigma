using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject utilityPanel;
    public GameObject dialogPanel;
    public ItemSlot[] itemSlot;
    public UtilitySlot[] utilitySlot;
    //public DialogSlot[] dialogSlot = new DialogSlot[1];
    private int lastThisWeaponSlotSelected = -1;
    private int lastThisUtlilitySlotSelected = -1;
    private bool isEPressed = false;
    private bool isUsePerformed = false;

    private WeaponSOLib weaponSOLib;
    private UtilitySOLib utilitySOLib;

    // Start is called before the first frame update
    void Start()
    {
        weaponSOLib = GameObject.Find("InventoryCanvas").GetComponent<WeaponSOLib>();
        utilitySOLib = GameObject.Find("InventoryCanvas").GetComponent<UtilitySOLib>();
        //find ItemPackage object by tag "Inventory"
        GameObject itemPackage = GameObject.FindGameObjectWithTag("Inventory");
        //do not destroy the object when the scene changes
        DontDestroyOnLoad(itemPackage);
        
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
            else if (Input.GetKeyDown(KeyCode.Alpha4)) key = KeyCode.Alpha4;
            else if (Input.GetKeyDown(KeyCode.Alpha5)) key = KeyCode.Alpha5;
            else if (Input.GetKeyDown(KeyCode.Alpha6)) key = KeyCode.Alpha6;
            else if (Input.GetKeyDown(KeyCode.E))
            {
                key = KeyCode.E;
                isEPressed = true;
            }

            switch (key)
            {
                case KeyCode.Alpha1:
                    Debug.Log("Pressed 1");
                    ActivateWeaponSlot(0);
                    break;
                case KeyCode.Alpha2:
                    Debug.Log("Pressed 2");
                    ActivateWeaponSlot(1);
                    break;
                case KeyCode.Alpha3:
                    Debug.Log("Pressed 3");
                    ActivateWeaponSlot(2);
                    break;
                case KeyCode.Alpha4:
                    Debug.Log("Pressed 4");
                    ActivateUtilitySlot(0);
                    break;
                case KeyCode.Alpha5:
                    Debug.Log("Pressed 5");
                    ActivateUtilitySlot(1);
                    break;
                case KeyCode.Alpha6:
                    Debug.Log("Pressed 6");
                    ActivateUtilitySlot(2);
                    break;
                case KeyCode.E:
                    if (lastThisUtlilitySlotSelected >= 0)
                    {
                        ActivateUtilitySlot(lastThisUtlilitySlotSelected);
                    }
                    break;
                default:
                    // Opcjonalnie: obs³uga innych klawiszy lub brak akcji
                    break;
            }
        }
        else
        {
            isEPressed = false;
        }
    }

    public void ActivateWeaponSlot(int index)
    {
        if (index >= 0 && index < itemSlot.Length)
        {
            DeselectAllWeaponSlots(); // Odznacz wszystkie sloty

            for (int i = 0; i < itemSlot.Length; i++)
            {
                for (int j = 0; j < weaponSOLib.weaponSOs.Length; j++)
                {
                    if (weaponSOLib.weaponSOs[j].itemName == itemSlot[i].itemName && i == lastThisWeaponSlotSelected)
                        weaponSOLib.weaponSOs[j].UnequipItem();
                }
            }

            for (int i = 0; i < weaponSOLib.weaponSOs.Length; i++)
            {
                //Debug.Log("Checking " + weaponSOLib.weaponSO[i].itemName);
                if (weaponSOLib.weaponSOs[i].itemName == itemSlot[index].itemName)
                {
                    //Debug.Log("found " + weaponSOLib.weaponSO[i].itemName);
                    weaponSOLib.weaponSOs[i].EquipItem();
                    Debug.Log("ubrano " + weaponSOLib.weaponSOs[i].itemName);
                }
            }

            itemSlot[index].SelectedShader.SetActive(true); // Aktywuj shader dla wybranego slotu
            itemSlot[index].thisItemSelected = true; // Oznacz slot jako wybrany
            lastThisWeaponSlotSelected = index; // Zapamiêtaj ostatni wybrany slot
        }
    }

    public void ActivateUtilitySlot(int index)
    {
        if (index >= 0 && index < utilitySlot.Length)
        {
            DeselectAllUtilitySlots(); // Odznacz wszystkie sloty

            utilitySlot[index].SelectedShader.SetActive(true); // Aktywuj shader dla wybranego slotu
            utilitySlot[index].thisItemSelected = true; // Oznacz slot jako wybrany

            if (lastThisUtlilitySlotSelected != index && !isEPressed)
            {
                Debug.Log("Nie u¿yto" + utilitySlot[index].itemName);
            }
            else
            {
                for (int i = 0; i < utilitySOLib.itemSOs.Length; i++)
                {
                    Debug.Log("Sprwadzam " + utilitySOLib.itemSOs[i].itemName);
                    if (utilitySOLib.itemSOs[i].itemName == utilitySlot[index].itemName)
                    {
                        Debug.Log("Znalaz³em " + utilitySOLib.itemSOs[i].itemName);
                        utilitySOLib.itemSOs[i].UseItem();
                        Debug.Log("U¿yto" + utilitySlot[index].itemName);
                        ClearUtilitySlot(index);
                    }
                }
            }
            if (isUsePerformed)
            {
                isUsePerformed = false;
            }
            else
            {
                lastThisUtlilitySlotSelected = index; // Zapamiêtaj ostatni wybrany slot
            }
        }
    }

    private void ClearUtilitySlot(int index)
    {
        if (index >= 0 && index < utilitySlot.Length)
        {
            isUsePerformed = true;
            lastThisUtlilitySlotSelected = -1;
            utilitySlot[index].thisItemSelected = false;
            utilitySlot[index].ClearSlot();
        }
    }
    public void AddWeaponItem(string itemName, Sprite itemSprite, ItemType itemType)
    {
        if(itemType == ItemType.Weapon)
        {
            Debug.Log("Added " + itemName + " dialog");

            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull == false)
                {
                    itemSlot[i].AddItem(itemName, itemSprite, itemType);
                    return;
                }
            }
        }

    }
    public void AddUtilityItem(string itemName, Sprite itemSprite, ItemType itemType)
    {
        if(itemType== ItemType.Consumable)
        {
            Debug.Log("Added " + itemName + " to inventory");

            for (int i = 0; i < utilitySlot.Length; i++)
            {
                if (utilitySlot[i].isFull == false)
                {
                    utilitySlot[i].AddItem(itemName, itemSprite, itemType);
                    return;
                }
            }
        }
    }

/*    public void AddItem(string itemName, Sprite itemSprite, ItemType itemType)
    {
        if (itemType == ItemType.Weapon)
        {
            Debug.Log("Added " + itemName + " dialog");

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
    }*/
    public void ShowInventoryFullDialog(string itemName, Sprite itemSprite, ItemType itemType)
    {
        //currentItem = item;

        if (dialogPanel != null)
        {
            dialogPanel.SetActive(true);
            for (int i = 0; i < weaponSOLib.weaponSOs.Length; i++)
            {
                if (weaponSOLib.weaponSOs[i].itemName == itemName)
                    weaponSOLib.weaponSOs[i].DialogSet(itemSprite);
            }
        }
    }
    public void HideInventoryFullDialog(string itemName)
    {
        //Debug.Log("przed null");
        if (dialogPanel != null)
        {
            //Debug.Log("po null");
            for (int i = 0; i < weaponSOLib.weaponSOs.Length; i++)
            {
                if (weaponSOLib.weaponSOs[i].itemName == itemName)
                weaponSOLib.weaponSOs[i].DialogClear();
                dialogPanel.SetActive(false);
            }
        }
    }

   

    /*public void ReplaceItem(int slotIndex, Item item)
      {

          itemSlot[slotIndex].AddItem(item.GetItemName(), item.GetItemSprite());
          itemSlot[slotIndex].isFull = true;
          Debug.Log("Replaced item in slot " + slotIndex);
      }*/

    public bool IsInventoryWeaponFull()
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
    public bool IsInventoryUtilityFull()
    {
        for (int i = 0; i < utilitySlot.Length; i++)
        {
            if (utilitySlot[i].isFull == false)
            {
                return false;
            }
        }
        return true;
    }
    public void DeselectAllWeaponSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].SelectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    public void DeselectAllUtilitySlots()
    {
        for (int i = 0; i < utilitySlot.Length; i++)
        {
            utilitySlot[i].SelectedShader.SetActive(false);
            utilitySlot[i].thisItemSelected = false;
        }
    }


}

public enum ItemType
{
    None,
    Consumable,
    Weapon,
};