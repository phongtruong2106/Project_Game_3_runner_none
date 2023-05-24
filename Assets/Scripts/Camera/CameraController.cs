using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float shakeDistance = 0.1f;
    public float shakeSpeed = 1;

    private Vector3 initialPosition;
    private Vector3 shakeOffset;

    private bool isShaking = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isShaking)
        {
            shakeOffset = new Vector3(0, Mathf.Sin(Time.time * shakeSpeed) * shakeDistance, 0);
            transform.position = initialPosition + shakeOffset;
        }
    }

    public void StartShaking()
    {
        isShaking = true;
    }

    public void StopShaking()
    {
        isShaking = false;
        transform.position = initialPosition;
    }
}