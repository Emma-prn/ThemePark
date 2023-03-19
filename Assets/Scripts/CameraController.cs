using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    public float zoomSpeed = 1f;
    public float minFOV = 10f;
    public float maxFOV = 60f;

    private Camera mainCamera;

    void Start(){
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(inputX, 0.0f, inputZ) * cameraSpeed;
        transform.position += movement;

        float fov = mainCamera.fieldOfView;

        if (Input.GetKey(KeyCode.Z))
        {
            fov -= zoomSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            fov += zoomSpeed * Time.deltaTime;
        }

        fov = Mathf.Clamp(fov, minFOV, maxFOV);

        mainCamera.fieldOfView = fov;
    }
}
