using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraControl : NetworkBehaviour
{
    private Transform vision;
    private Vector3 offset;
    private float rotation = 0;
    private float elevation = 0;

    public float sensitivity = 100;

    void Start()
    {
        if (!hasAuthority) return;

        //Cursor.lockState = CursorLockMode.Locked;

        Camera camera = FindObjectOfType<Camera>();
        vision = camera.transform;

        offset = isServer ? 
            new Vector3(0, 0, 0) : new Vector3(0, 2, 0);
    }

    void Update()
    {
        if (!hasAuthority) return;
        
        float xAxis = Input.GetAxis("Mouse X");
        float yAxis = Input.GetAxis("Mouse Y");

        vision.position = transform.position + offset;

        rotation = xAxis * sensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * rotation);

        elevation -= yAxis * sensitivity * Time.deltaTime;
        elevation = Mathf.Clamp(elevation, -90, 13);

        vision.rotation = transform.rotation;
        vision.Rotate(Vector3.right * elevation);
    }
}
