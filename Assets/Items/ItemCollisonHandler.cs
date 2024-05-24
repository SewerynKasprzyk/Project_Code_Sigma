using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollisonHandler : MonoBehaviour
{
    [SerializeField] private GameObject DialogWindowPanel;
    [SerializeField] private InventoryManager inventoryManager;
    private Item currentItem;
    //chyba do zmiany
    private bool isPressedE = false; 
    private bool isPressed1 = false; 
    private bool isPressed2 = false; 
    private bool isPressed3 = false; 
    private bool actionPerformed = false; 

    private void Start()
    {
        HidePickUpDialog();
    }

    private void Update()
    {
        if(DialogWindowPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            isPressedE = true;
            Debug.Log("E"); 
        }
        else
        {
            isPressedE = false;
            actionPerformed = false;
        }
        if(!actionPerformed && isPressedE)
        {
            Debug.Log("Przesz³o");
            inventoryManager.ReplaceItem(0, currentItem);
            Debug.Log("Zmiana");
            Destroy(currentItem.gameObject);
            actionPerformed = true;
        }
    }

    public void ShowPickUpDialog(Item item)
    {
        currentItem = item;

        if (DialogWindowPanel != null)
        {
            DialogWindowPanel.SetActive(true);
        }
    }

    public void HidePickUpDialog()
    {
       if(DialogWindowPanel != null)
        {
            DialogWindowPanel.SetActive(false);
        }
    }
}