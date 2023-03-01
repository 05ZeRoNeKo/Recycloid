using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDropTouch : MonoBehaviour
{
    [SerializeField]
    private InputAction touch;
    [SerializeField]
    private float touchDragPhysicsSpeed = 10;
    [SerializeField]
    private float touchDragSpeed = .1f;
    [SerializeField]
    private float distanceFromCamera = 5f;

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        touch.Enable();
        touch.performed += TouchPressed;
    }
    private void OnDisable()
    {
        touch.performed -= TouchPressed;
        touch.Disable();
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if(hit.collider.gameObject.CompareTag("Biodegradable") || hit.collider.gameObject.CompareTag("Non-Biodegradable") || hit.collider.gameObject.CompareTag("Recyclable"))
                    StartCoroutine(DragUpdateTouch(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator DragUpdateTouch(GameObject clickedObject)
    {
        float initialDistance = Vector3.Distance(clickedObject.transform.position, new Vector3(0, distanceFromCamera, 0));
        clickedObject.TryGetComponent<Rigidbody>(out var rb);

        while (touch.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.position.ReadValue());

            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.velocity = direction * touchDragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position,
                    ray.GetPoint(initialDistance), ref velocity, touchDragSpeed);
                yield return null;
            }
        }
    }
}
