using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Gamma : MonoBehaviour
{

    private SpriteRenderer[] spriteRenderers;

    private void Start()
    {
        spriteRenderers = FindObjectsOfType<SpriteRenderer>();
        UnityEngine.Debug.Log(spriteRenderers.Length);
    }

    public void AdjustBrightness(float gamma)
    {

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = new Color(gamma, gamma, gamma, spriteRenderer.color.a);
        }
    }
}