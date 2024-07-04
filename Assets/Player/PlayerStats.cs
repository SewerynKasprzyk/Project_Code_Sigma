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

    private float lastDamageTime = -1;
    public float damageCooldown = 1.0f;

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
        if (damage <= 0) return;

        playerHp -= damage;
        //Debug.Log("Current: " + playerHp);

        // Tworzenie popupu z obra�eniami
        GameObject popUp = Instantiate(popUpPrefab, transform.position, Quaternion.identity);
        popUp.GetComponentInChildren<TMP_Text>().text = damage.ToString();

        if (playerHp <= 0)
        {
            playerCtrl.DeathAnimation();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyStats enemyCollision = collision.gameObject.GetComponent<EnemyStats>();
        if (enemyCollision != null)
        {
            // Odejmowanie zwyk�ych obra�e�
            TakeDamage(enemyCollision.enemyDamage);
            GameObject popUp = Instantiate(popUpPrefab, transform.position, Quaternion.identity);

            // Sprawdzenie, czy up�yn�� wystarczaj�cy czas od ostatniego otrzymania obra�e� od broni
            if (Time.time - lastDamageTime > damageCooldown)
            {
                popUp.GetComponentInChildren<TMP_Text>().text = enemyCollision.enemyDamage.ToString();
            }
            
            popUp.GetComponentInChildren<TMP_Text>().text = enemyCollision.enemyWeaponDamage.ToString();
        }
    }


}
