using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    // public float groundDrag;
    // public float jumpForce;
    // public float jumpCooldown;
    // public float airMultiplier;

    [HideInInspector] public float sprintSpeed;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    CharacterController characterController;
    public float gravity = -9.81f;
    // public PlayableDirector Timeline3;

    public GameObject Cursor;
    // public PlayableDirector LastAnim;
    // public PlayableDirector TimeKey;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // ground check
        // grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.3f, whatIsGround);

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        if (isSprinting)
        {
            moveSpeed = 7.5f;
        }
        else
        {
            moveSpeed = 5;
        }

        MyInput();
        if (!GameManager.instance.isInventoryOpen)
        {
            MovePlayer();
            Cursor.SetActive(true);
        }
        else
        {
            Cursor.SetActive(false);
        }
    }

    private void FixedUpdate()
    {

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
{
    // Calculate movement direction
    moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
    moveDirection *= moveSpeed;

    // Check if the player is grounded before moving
    if (characterController.isGrounded)
    {
        moveDirection.y = 0f;
    }
    else
    {
        // Apply gravity
        moveDirection.y += gravity * Time.deltaTime;
    }

    // Move the player
    characterController.Move(moveDirection * Time.deltaTime);
}

}
