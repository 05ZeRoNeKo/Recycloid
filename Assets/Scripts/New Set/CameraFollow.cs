using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float followDelay;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(gameObject.transform.position, player.position, followDelay);
        Camera.main.transform.LookAt(transform);
    }
}
