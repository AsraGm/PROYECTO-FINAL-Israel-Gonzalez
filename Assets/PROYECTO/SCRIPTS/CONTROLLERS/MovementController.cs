using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float crouchSpeed = 4;
    private float walkSpeed = 6;
    private float runSpeed = 8;

    [Header("Jump Settings")]
    private float jumpForce = 15f;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpRequested;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ConfigurePhysics();
    }

    private void ConfigurePhysics()
    {
        rb.useGravity = true;
        rb.drag = 5f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        HandleInput();
        CheckGroundStatus();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    private void HandleInput()
    {
        // Inputs mantenidos como m�todos separados para claridad
        moveDirection = CalculateMovementDirection();

        if (JumpInputPressed() && isGrounded)
        {
            jumpRequested = true;
        }
    }

    private Vector3 CalculateMovementDirection()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        return (forward * VerticalAxis() + right * HorizontalAxis()).normalized;
    }

    private void HandleMovement()
    {
        float currentSpeed = GetTargetSpeed();
        Vector3 targetVelocity = moveDirection * currentSpeed;
        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
    }

    private void HandleJump()
    {
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            rb.AddForce(Physics.gravity * 2f, ForceMode.Acceleration);
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
    }

    # region M�todos de input 
    private float HorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }

    private float VerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }

    private bool JumpInputPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private bool RunInputPressed()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    private bool CrouchInputPressed()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }
    #endregion

    private float GetTargetSpeed()
    {
        if (RunInputPressed()) return runSpeed;
        if (CrouchInputPressed()) return crouchSpeed;
        return walkSpeed;
    }

   
}
