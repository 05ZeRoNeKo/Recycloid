using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraLook : MonoBehaviour
{
    public float sensitivityY;

    private PlayerControls playerControls;

    Vector2 playerLook;
    float inputY;
    float rotationY;

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
        LookInput();
    }
    private void FixedUpdate()
    {
        CameraRotate();
    }

    private void LookInput()
    {
        playerLook = playerControls.Player.Look.ReadValue<Vector2>();

        inputY = playerLook.x * Time.deltaTime * sensitivityY;

        rotationY += inputY;
    }
    private void CameraRotate()
    {
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    
}
