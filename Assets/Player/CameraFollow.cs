using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    //public Transform Target;

    void Start()
    {
        // Szuka obiektu z tagiem "Player" i zapisuje jego transformacjê
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        // Ustawia pocz¹tkow¹ pozycjê kamery wzglêdem znalezionego gracza
        if (player != null)
        {
            transform.position = player.position;
        }
    }

    void Update()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player != null)
        {
            Vector3 newPos = new Vector3(player.position.x, player.position.y, -10);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed); //* Time.deltaTime
        }
    }
}
