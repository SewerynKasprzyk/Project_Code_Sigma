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
        Debug.Log("acc1");
        this.itemSprite = itemSprite;
        Debug.Log("acc2");
        itemImage.sprite = itemSprite;
        Debug.Log("acc3");
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
