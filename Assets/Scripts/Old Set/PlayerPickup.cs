using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    [Header("Object Reference")]
    public Transform Hand; //for positioning of pickup item
    public Transform throwPosition; //where the object will be positioned before throwing
    public Camera playerCamera; //origin of ray

    [Space]
    [Header("Pickup Values")]
    public float pickupRange; //determines length of ray
    public LayerMask pickupLayer; //limits pickup to objects with this layer

    [Space]
    public float throwForce; // strength of throw

    private PlayerControls playerControls;

    private GameObject currentObject;
    private Rigidbody currentRigidbody;
    private Collider currentCollider;

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

    private void Update()
    {
        PickUp();
        Throw();
    }

    private void PickUp()
    {
        if (currentObject)//updates pick up item position to hand
        {
            currentObject.transform.position = Hand.position;
            currentObject.transform.rotation = Hand.rotation;

            return;
        }

        Ray pickupRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(pickupRay, out RaycastHit hitInfo, pickupRange, pickupLayer))
        {
            if (playerControls.Player.Grab.IsPressed())
            {
                //assigns item to player
                currentObject = hitInfo.collider.gameObject;
                currentRigidbody = currentObject.GetComponent<Rigidbody>();
                currentCollider = currentObject.GetComponent<Collider>();

                //attaches pick up item to hand with position and rotation
                currentObject.transform.parent = Hand;
                currentObject.transform.localPosition = Vector3.zero;
                currentObject.transform.localEulerAngles = Vector3.zero;

                //removes item from any physics
                currentRigidbody.isKinematic = true;
                currentCollider.enabled = false;
            }
        }
    }
    private void Throw()
    {
        if (!currentObject)
        {
            return;
        }

        float throwStrength = throwForce * Time.deltaTime;

        if (playerControls.Player.Throw.IsPressed())
        {
            currentObject.transform.parent = null; //separates pick up item from hand

            currentRigidbody.isKinematic = false; //returns item to physics calculation
            currentObject.transform.position = throwPosition.position;
            currentRigidbody.AddForce(currentObject.transform.forward * throwStrength, ForceMode.VelocityChange);//adds throw

            currentCollider.enabled = true;

            //resets assigned item
            currentCollider = null;
            currentRigidbody = null;
            currentObject = null;
        }
    }
}
