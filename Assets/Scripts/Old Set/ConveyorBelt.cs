using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float beltSpeed;

    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPos = rigidBody.position;
        rigidBody.position += transform.forward * beltSpeed * Time.fixedDeltaTime;
        rigidBody.MovePosition(currentPos);
    }
}
