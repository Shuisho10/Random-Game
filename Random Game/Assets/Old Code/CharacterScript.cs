using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [Header("Assign")]
    [SerializeField] LayerMask terrainLayer;
    [Header("Movement")]
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 550f;

    float verticalInput, horizontalInput, fallingVel;
    Vector2 normalizedAxisInput;
    Vector3 moveDir;
    Vector3 fallDir = new Vector3(0f, -1f, 0f);
    bool isSliding;
    bool readyToJump;
    CharacterController charControl;

    void Awake()
    {
        charControl = GetComponent<CharacterController>();
    }


    void Update()
    {
        GetInput();
        
    }
    void FixedUpdate()
    {
        Movement();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (1<<hit.collider.gameObject.layer == terrainLayer)
        {
            float Angle = Vector3.Angle(Vector3.up, hit.normal);

            if (Angle > charControl.slopeLimit && hit.normal.y >= 0)
            {
                //Steep Slope or wall
                fallDir = Vector3.ProjectOnPlane(Vector3.down, hit.normal);
                isSliding = true;
            }
            else if (hit.normal.y >= 0)
            {
                //Floor
                fallDir = Vector3.down;
                isSliding = false;
                Invoke("PrepareJump", 0.05f);
            }         
            else
            {
                //Ceiling
                fallDir = Vector3.down;
                if (fallingVel < 0 && Angle > 92) fallingVel += Mathf.Abs(charControl.velocity.y * Mathf.Sin(Angle));
                isSliding = false;
            }
        }
        else fallDir = Vector3.down;
    }


    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        normalizedAxisInput = new Vector2(Mathf.Abs(horizontalInput), Mathf.Abs(verticalInput)).normalized;
    }

    void Movement()
    {
        moveDir = (transform.forward * verticalInput * normalizedAxisInput.y + transform.right * horizontalInput * normalizedAxisInput.x) * speed;
        if (charControl.isGrounded && !isSliding) fallingVel = 2f;
        else  fallingVel += 30f * Time.deltaTime; 
        if (Input.GetKey("space") && readyToJump && !isSliding) Jump();
        charControl.Move(moveDir * Time.deltaTime + fallingVel * fallDir * Time.deltaTime);
    }

    void Jump()
    {
        fallingVel = -jumpForce;
        readyToJump = false;
    }

    void PrepareJump()
    {
        if(charControl.isGrounded) readyToJump = true;
    }



}
