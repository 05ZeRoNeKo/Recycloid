using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GyroCamera : MonoBehaviour
{
    //public Transform camHolder;
    public Transform camOrientation;

    public float smoothing;
    public float sensitivityX;
    public float sensitivityY;
    private float gyroX;
    private float gyroY;

    float AccelerometerUpdateInterval  = 1.0f / 60.0f;
    float LowPassKernelWidthInSeconds = 500000.0f; 
    float LowPassFilterFactor;

    private void OnEnable()
    {
        InputSystem.EnableDevice(Accelerometer.current.device);
    }
    private void OnDisable()
    {
        InputSystem.DisableDevice(Accelerometer.current.device);
    }
    private void Update()
    {
        GyroLookInput();
    }
    private void LateUpdate()
    {
        GyroCameraRotate();
    }

    private void GyroLookInput()
    {
        Vector3 accel = Accelerometer.current.acceleration.ReadValue();
        gyroX = accel.y;
        gyroY = -accel.x;
    }
    private void GyroCameraRotate()
    {
        LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
        Quaternion newInput = new Quaternion(gyroX * sensitivityY, gyroY * sensitivityX, 0, 0);

        camOrientation.localRotation = Quaternion.Euler(gyroX * sensitivityY, gyroY * sensitivityX, 0);
        camOrientation.rotation = Quaternion.Slerp(camOrientation.rotation, newInput, LowPassFilterFactor);
        transform.rotation = Quaternion.Lerp(transform.rotation, camOrientation.rotation, smoothing);
    }
}
