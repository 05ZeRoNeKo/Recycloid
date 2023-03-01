using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Object Reference")]
    public Transform Hand; //for positioning of pickup item
    public Transform throwPosition; //where the object will be positioned before throwing
    public GameObject throwPoint; //indication of where the object will land (this is a lazy way of doing it)

    public int layerNumber; //limits pickup to objects with this layer
    public float throwForce; // strength of throw

    private PlayerControls playerControls;

    //variables to manage pickups
    private GameObject currentObject;
    private Rigidbody currentRigidbody;
    private Collider currentCollider;

    private bool canThrow = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }


    private void Update() //inputs here for responsiveness
    {
        if (!currentObject)
        {
            throwPoint.SetActive(false);
        }

        if (playerControls.Player.Throw.IsPressed())
        {
            canThrow = true;
        }
    }
    private void FixedUpdate() //for physics timestep
    {
        Throw();
    }

    public void OnTriggerEnter(Collider other) //for pick-up system
    {
        if(other.gameObject.layer == layerNumber)
        {
            if (currentObject)//updates pick up item position to hand
            {
                currentObject.transform.position = Hand.position;
                currentObject.transform.rotation = Hand.rotation;

                return;
            }

            //assigns item to player
            currentObject = other.gameObject;
            currentRigidbody = currentObject.GetComponent<Rigidbody>();
            currentCollider = currentObject.GetComponent<Collider>();

            //attaches pick up item to hand with position and rotation
            currentObject.transform.parent = Hand;
            currentObject.transform.localPosition = Vector3.zero;
            currentObject.transform.localEulerAngles = Vector3.zero;

            //removes item from any physics
            currentRigidbody.isKinematic = true;
            currentCollider.enabled = false;

            throwPoint.SetActive(true);
        } 
    }
    private void Throw() //for throwing system
    {
        if (!currentObject)
        {
            canThrow = false;
            return;
        }

        float throwStrength = throwForce * Time.deltaTime;

        if (canThrow)
        {
            currentObject.transform.parent = null; //separates pick up item from hand

            currentRigidbody.isKinematic = false; //returns item to physics calculation
            currentObject.transform.position = throwPosition.position;
            currentRigidbody.velocity = new Vector3(0, 0, 0);
            currentRigidbody.AddForce(10f * throwStrength * throwPosition.forward, ForceMode.VelocityChange);//adds throw

            currentCollider.enabled = true;

            //resets assigned item
            currentCollider = null;
            currentRigidbody = null;
            currentObject = null;

            throwPoint.SetActive(false);

            canThrow = false;
        }
    }
}
