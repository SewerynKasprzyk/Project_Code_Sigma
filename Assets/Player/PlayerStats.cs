using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update

    public float playerHp;
    public float playerMovementSpeed;
    public float playerDamage;

    public Rigidbody2D player;

    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current: " + playerHp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyStats enemyCollision = collision.gameObject.GetComponent<EnemyStats>();
        if(enemyCollision != null)
        {
            playerHp = playerHp - enemyCollision.enemyDamage;
            Debug.Log("Current: " + playerHp);
        }
    }
}
