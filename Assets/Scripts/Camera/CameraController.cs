using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float pitch;
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    public float yawSpeed;

    private float _currentZoom;
    private float _currentYaw;

    public void Start()
    {
        pitch = 1.15f;
        zoomSpeed = 4;
        minZoom = 5;
        maxZoom = 10;
        yawSpeed = 100;

        _currentYaw = 0;
        _currentZoom = 10f;
    }

    public void Update()
    {
        //«ум камеры
        _currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, minZoom, maxZoom);

        _currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    public void LateUpdate()
    {
        transform.position = target.position - offset * _currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, -_currentYaw);
    }
}
