using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    Vector3 movementDirection;
    Vector3 velocity;

    CharacterController charController;

    [SerializeField] float speed = 12f;
    [SerializeField] float jumpHeight;
    float gravity = -9.81f;

    [SerializeField] Transform groundCheck;
    float groundDistance = .4f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // TODO: check if movement update is avaliable

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        // movement
        movementDirection = transform.right * xMovement + transform.forward * zMovement;
        charController.Move(movementDirection * speed * Time.deltaTime);

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // gravity
        velocity.y += gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);
    }
}
