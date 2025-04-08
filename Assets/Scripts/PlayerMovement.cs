using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

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

    private InputAction Pause;
    [SerializeField] private GameObject PausePopUp;
    static public bool GamePaused;

    public float sensX;
    public float sensY;

    float xRotation;
    float yRotation;

    Transform cam;
    Camera camComponent;

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
        Pause = playerInput.currentActionMap.FindAction("Pause");
        Pause.performed += ctx => PauseGame();

        Cursor.lockState = CursorLockMode.Locked;

        cam = transform.GetChild(1);
        camComponent = cam.GetComponent<Camera>();
    }

    private void Update()
    {
        rotateCamera();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        verticalInput = Movement.ReadValue<float>();
        horizontalInput = Turning.ReadValue<float>();

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        checkForInteractable();
    }

    [SerializeField] private float interactRange = 3f;

    private IInteractable checkForInteractable()
    {
        Ray ray = camComponent.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange) && hit.transform.tag == "Clickable")
        {
            CursorBehavior.objectClickable = true;
            return hit.transform.GetComponent<IInteractable>();
        }
        else
        {
            CursorBehavior.objectClickable = false;
        }

        return null;
    }

    private void TryInteract() ///For now ive just got it as simple proximity interaction, we can change it if necessary
    {
        /*
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRange);

        foreach (var hit in hits)
        {
            var interactable = hit.GetComponent<IInteractable>();
            if (interactable != null && CursorBehavior.objectClickable)
            {
                interactable.OnInteract();
                return; // Will only interacts with the first object found
            }
        }*/

        IInteractable interactable = checkForInteractable();

        if (interactable != null)
        {
            interactable.OnInteract();
        }
    }

    private void rotateCamera()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            cam.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
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

    private void PauseGame()
    {

        if (!GamePaused)
        {
            GamePaused = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;

            PausePopUp.SetActive(true);
        }

    }
}
