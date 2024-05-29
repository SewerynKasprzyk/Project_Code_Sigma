using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpacitySwap : MonoBehaviour
{
    private bool swap = false;

    // Start is called before the first frame update
    void Start()
    {
        Image image = gameObject.GetComponent<Image>();
        Color tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        Image image = gameObject.GetComponent<Image>();
        if (image.sprite != null && !swap)
        {
            swap = true;
            Color tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }
    }
}
