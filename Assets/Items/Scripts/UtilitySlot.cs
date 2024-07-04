using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtilitySlot : MonoBehaviour
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

    private void Start()
    {

    }
    public void AddItem(string itemName, Sprite itemSprite, ItemType itemType)
    {
        this.itemType = itemType;
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        isFull = true;
    }
}


