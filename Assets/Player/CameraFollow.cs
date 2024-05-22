using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(Target.position.x, Target.position.y, -10);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed); //* Time.deltaTime
    }
}
