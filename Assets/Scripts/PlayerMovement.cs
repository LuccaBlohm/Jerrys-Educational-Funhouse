using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [SerializeField] private PlayerInput playerInput;
    private InputAction Movement;
    private InputAction Turning;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput.currentActionMap.Enable();
        Movement = playerInput.currentActionMap.FindAction("Movement");
        Turning = playerInput.currentActionMap.FindAction("Turning");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        verticalInput = Movement.ReadValue<float>();
        horizontalInput = Turning.ReadValue<float>();

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
