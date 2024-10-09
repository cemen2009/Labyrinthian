using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    Vector3 movementDirection;

    // jump variables
    [SerializeField] float jumpForce, jumpCooldown, airMultiplier;
    bool readyToJump = true;

    [SerializeField] Transform orientation;

    float horizontalInput, verticalInput;

    Rigidbody rb;

    [SerializeField] LayerMask groundLayer;
    float characterHeight, shiftError = .1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterHeight = transform.Find("Object").GetComponent<CapsuleCollider>().height;

        rb.freezeRotation = true;
    }

    private void Update()
    {
        GetInput();

        SpeedClamp();
    }

    private void FixedUpdate()
    {
        ApplyInput();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Jump") && IsGrounded() && readyToJump)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ApplyInput()
    {
        // apply input according to orientation transform
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (IsGrounded())
            rb.AddForce(movementDirection.normalized * movementSpeed, ForceMode.Force);
        else
            rb.AddForce(movementDirection.normalized * movementSpeed * airMultiplier, ForceMode.Force);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, characterHeight / 2 + shiftError, groundLayer);
    }

    private void SpeedClamp()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 clampedVelocity = flatVelocity.normalized * movementSpeed;
            rb.velocity = new Vector3(clampedVelocity.x, rb.velocity.y, clampedVelocity.z);
        }
    }

    private void Jump()
    {
        // reset vertical velocity
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
