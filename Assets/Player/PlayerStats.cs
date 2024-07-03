using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update

    public float playerHp;
    public float playerMovementSpeed;
    public float playerDamage;

    public GameObject popUpPrefab;

    public Rigidbody2D player;

    private PlayerCtrl playerCtrl;

    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        playerCtrl = GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Current: " + playerHp);
    }

    public void TakeDamage(float damage)
    {
        playerHp -= damage;
        Debug.Log("Current: " + playerHp);

        if (playerHp <= 0)
        {
            playerCtrl.DeathAnimation();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyStats enemyCollision = collision.gameObject.GetComponent<EnemyStats>();
        if(enemyCollision != null)
        {
            playerHp = playerHp - enemyCollision.enemyDamage;
            Debug.Log("Current: " + playerHp);

            GameObject popUp = Instantiate(popUpPrefab, transform.position, Quaternion.identity);
            if(enemyCollision.enemyDamage > 0)
            {
                popUp.GetComponentInChildren<TMP_Text>().text = enemyCollision.enemyDamage.ToString();
            }           
            popUp.GetComponentInChildren<TMP_Text>().text = enemyCollision.enemyWeaponDamage.ToString();

        }
    }
}
