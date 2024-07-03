using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //Data
    public string itemName;
    public Sprite itemSprite;
    public bool isFull;

    //Slot
    [SerializeField] private Image itemImage;


    public void AddItem(string itemName, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        isFull = true;
    }

    public void SelectedInventoryItem()
    {
        if(Event.current.Equals(Event.KeyboardEvent("1")))
        {
            Debug.Log("1");
        }
        if (Event.current.Equals(Event.KeyboardEvent("2")))
        {
            Debug.Log("2");
        }
        if (Event.current.Equals(Event.KeyboardEvent("3")))
        {
            Debug.Log("3");
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
