using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    [HideInInspector] public float sprintSpeed;

    // [Header("Ground Check")]
    // public float playerHeight;
    // public LayerMask whatIsGround;
    // bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    CharacterController characterController;
    public float gravity = -9.81f;
    public GameObject Cursor;
    public Joystick joystick;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {


        bool isSprinting = Input.GetKey(KeyCode.LeftShift) ||(joystick.Horizontal > 0.5f || joystick.Vertical > 0.5f);
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
        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;
    }

    private void MovePlayer()
    {

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection *= moveSpeed;


        characterController.Move(moveDirection * Time.deltaTime);
    }
    // public void SetJoystickInput(Vector2 input)
    // {
    //     horizontalInput = input.x;
    //     verticalInput = input.y;
    //     Debug.Log("move");
    // }


}
