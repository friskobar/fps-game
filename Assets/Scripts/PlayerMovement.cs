using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public CapsuleCollider cap;
    [Header("Basic Movement")]
    public Vector3 moveInput = Vector3.zero;
    public Vector3 wishVel = Vector3.zero;
    public float maxSpeed = 2;
    [Header("Crouch")]
    public bool crouching;
    public float crouchSpeed;
    public float crouchHeight = 1;
    public float standHeight = 2;
    [Header("Jump")]
    public Transform groundCheck;
    public bool isGrounded = false;
    public bool wishJump = false;
    public float jumpForce = 8;
    public LayerMask groundLayer;

    [Header("UI")]
    public TMPro.TextMeshProUGUI SpeedCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
        groundCheck = transform.Find("groundCheck");
        groundLayer = LayerMask.NameToLayer("Ground");
        crouchSpeed = maxSpeed / 3;
    }

    void Update()
    {
        MyInput();
        updateGrounded();
        UpdateGUI();
    }

    void updateGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, .5f, groundLayer);
    }

    void FixedUpdate()
    {
        Movement();
    }
    void UpdateGUI()
    {
        SpeedCounter.text = "Speed: " + rb.velocity.magnitude;
    }

    void Movement()
    {
        rb.velocity = transform.forward * wishVel.x + transform.right * wishVel.z + transform.up * rb.velocity.y;
    }

    void MyInput()
    {
        moveInput = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        if (!crouching)
        {
            wishVel = moveInput * maxSpeed;
            cap.height = 2;
        }
        else
        {
            wishVel = moveInput * crouchSpeed;
            cap.height = 1;
        }
        if(Input.GetButtonDown("Jump") && isGrounded)
            Jump();

        crouching = Input.GetButton("Crouch");
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }
}