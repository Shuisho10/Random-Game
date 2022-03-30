using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [Header("Assign")]
    [SerializeField] LayerMask terrainLayer;
    CharacterController charControl;
    float verticalInput, horizontalInput;
    Vector2 normalizedAxisInput;
    [Header("Movement")]
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 550f;
    Vector3 moveDir;


    private void Awake()
    {
        charControl = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }


    /*
     * Emulate AWSD as a 2D Axis
     */
    private void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        normalizedAxisInput = new Vector2(Mathf.Abs(horizontalInput), Mathf.Abs(verticalInput)).normalized;
    }


    /* 
     * 
     */
    private void Movement()
    {
        moveDir = (transform.forward * verticalInput * normalizedAxisInput.y + transform.right * horizontalInput * normalizedAxisInput.x) * speed;
        charControl.Move(moveDir * Time.deltaTime);
    }
}
