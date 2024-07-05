using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//make a reference to PlayerStats.cs file 


public class Health : MonoBehaviour
{
    public Slider slider;
    public TMP_Text healthText;
    private PlayerStats playerStats; 

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats != null)
        {
            // Initialize your slider or other UI elements with playerStats values
            slider.maxValue = playerStats.playerHp;
            slider.value = playerStats.playerHp;
        }

        //save the slider object so it is not destroyed when the scene changes
        DontDestroyOnLoad(slider.transform.root.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player HP: " + playerStats.playerHp);
        if (playerStats != null)
        {
            // Update your UI elements based on the current playerStats values
            slider.value = playerStats.playerHp;
            healthText.text = playerStats.playerHp.ToString("0");


        }
    }
}
