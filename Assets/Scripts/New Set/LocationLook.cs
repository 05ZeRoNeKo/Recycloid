using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLook : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.transform.LookAt(this.transform);
        }
    }
}
