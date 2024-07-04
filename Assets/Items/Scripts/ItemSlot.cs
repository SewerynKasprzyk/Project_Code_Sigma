using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //Data
    public bool isFull;
    public string itemName;
    public Sprite itemSprite;
    public ItemType itemType;

    //Slot
    [SerializeField] private Image itemImage;
    public GameObject SelectedShader;
    public bool thisItemSelected;
    public int slotIndex;

    private InventoryManager inventoryManager;
    private WeaponSOLib weaponSOLib;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        //weaponSOLib = GameObject.Find("InventoryCanvas").GetComponent<WeaponSOLib>();
    }
    public void AddItem(string itemName, Sprite itemSprite, ItemType itemType)
    {
        this.itemType = itemType;
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        isFull = true;
/*        Debug.Log("itemslot");

        for (int i = 0; i < weaponSOLib.weaponSO.Length; i++)
        {
            Debug.Log("Checking " + weaponSOLib.weaponSO[i].itemName);
            if (weaponSOLib.weaponSO[i].itemName == this.itemName)
            {
                Debug.Log("found " + weaponSOLib.weaponSO[i].itemName);
                weaponSOLib.weaponSO[i].EquipItem();
            }
        }*/
        
    }

    
}

 
