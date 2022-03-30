using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform camTr;
    Camera cam;

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        cam.transform.position = camTr.position;
    }
}
