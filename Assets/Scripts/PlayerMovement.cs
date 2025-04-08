using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public bool isInWaterEffectZone;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [SerializeField] private PlayerInput playerInput;
    private InputAction Movement;
    private InputAction Turning;

    private InputAction Interact;

    private InputAction Jump;

    private bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput.currentActionMap.Enable();
        Movement = playerInput.currentActionMap.FindAction("Movement");
        Turning = playerInput.currentActionMap.FindAction("Turning");
        Interact = playerInput.currentActionMap.FindAction("Interact");
        Interact.performed += ctx => TryInteract();
        Jump = playerInput.currentActionMap.FindAction("Jump");
        Jump.performed += ctx => TryJump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        verticalInput = Movement.ReadValue<float>();
        horizontalInput = Turning.ReadValue<float>();

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    [SerializeField] private float interactRange = 2f;

    private void TryInteract() ///For now ive just got it as simple proximity interaction, we can change it if necessary
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRange);

        foreach (var hit in hits)
        {
            var interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.OnInteract();
                return; // Will only interacts with the first object found
            }
        }
    }

    private void TryJump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            // If the ray hits something within a distance of 1.1f, the player is grounded
            if (hit.distance < 2f)
            {
                // Apply the jump force
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaterEffectZone"))
        {
            isInWaterEffectZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WaterEffectZone"))
        {
            isInWaterEffectZone = false;
        }
    }

}
