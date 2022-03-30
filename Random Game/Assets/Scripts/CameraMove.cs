using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform camTr;
    public Camera cam;

    void Update()
    {
        cam.transform.position = camTr.position;
    }
}
