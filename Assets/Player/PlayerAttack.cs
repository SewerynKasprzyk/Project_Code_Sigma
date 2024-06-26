using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;

    private bool attacking = false;

    private float timeToAttack = 0.2f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {       
       attackArea = transform.GetChild(0).gameObject;
       attackArea.SetActive(attacking);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if(attacking)
        {
            timer += Time.deltaTime;
            if(timer >= timeToAttack)
            {
                attacking = false;
                attackArea.SetActive(attacking);
                timer = 0f;
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
    }
}
