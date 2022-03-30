using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xSens = 10f, ySens = 10f;
    bool inMenu;
    float xRotation, yRotation;
    Transform playerTr;
    public Camera cam;

    void Awake()
    {
        playerTr = transform;
    }

    void Start()
    {
        xRotation = 0f;
        yRotation = 0f;
        inMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(!inMenu) CamRotate();
    }

    void CamRotate()
    {
        xRotation += Input.GetAxis("Mouse X") * xSens;
        yRotation -= Input.GetAxis("Mouse Y") * ySens;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);
        playerTr.transform.rotation = Quaternion.Euler(0f, xRotation, 0f);
    }

    public void LockCamera()
    {
        inMenu = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCamera()
    {
        inMenu = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
