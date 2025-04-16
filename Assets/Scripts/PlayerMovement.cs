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

    private InputAction Pause;
    [SerializeField] private GameObject PausePopUp;
    static public bool GamePaused;

    public float sensX;
    public float sensY;

    float xRotation;
    float yRotation;

    Transform cam;
    public Camera camComponent;

    public int rotationOffset;

    public ItemSprite itemHeld;

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
        camComponent = Camera.main;
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

    [SerializeField] private float interactRange = 5f;

    private Transform checkForInteractable()
    {
        Transform interactableTransform = null;
        IInteractable interactable = null;




        if (!GamePaused)
        {
            Ray ray = camComponent.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactRange))
            {
                string hitName = hit.transform.name;

                if (hitName == "FinalDoor" || hitName == "ExitEntrance" || hitName == "01_low" || hitName == "03_low")
                {
                    Debug.Log("Interactable found: " + hitName);
                }

                interactableTransform = hit.transform;
                interactable = interactableTransform.GetComponent<IInteractable>();
            }

            // switches cursor to clicking state
            if (interactable != null)
            {
                CursorBehavior.objectClickable = true;
                return interactableTransform;
            }
            else
            {
                CursorBehavior.objectClickable = false;
            }

        }

        return null;
    }

    private void TryInteract() ///For now ive just got it as simple proximity interaction, we can change it if necessary
    {
        Debug.Log("Interaction attempted");


        // Collider[] hits = Physics.OverlapSphere(transform.position, interactRange);

        // foreach (var hit in hits)
        // {
        //     Debug.Log("Interactable found");
        //     var interactable = hit.GetComponent<IInteractable>();
        //     if (interactable != null && CursorBehavior.objectClickable)
        //     {
        //         interactable.OnInteract();
        //         return; // Will only interacts with the first object found
        //     }
        // }

        // ui does not seem to block the raycast otherwise
        if (CursorBehavior.overUI)
        {
            return;
        }

        Transform interactableTransform = checkForInteractable();

        if (interactableTransform != null)
        {
            IInteractable interactable = interactableTransform.GetComponent<IInteractable>();
            ItemSprite item = interactableTransform.GetComponent<ItemSprite>();

            tryItemPickUp(item);
            Debug.Log("Interactable found");
            interactable.OnInteract();
        }
        else
        {
            if (itemHeld != null)
            {
                itemHeld.Drop();
                itemHeld = null;
            }
        }
    }

    private void tryItemPickUp(ItemSprite item)
    {

        if (item != null)
        {
            if (itemHeld != null)
            {
                itemHeld.Drop();
            }
            item.AttachToPlayer(transform);
            itemHeld = item;
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

            cam.rotation = Quaternion.Euler(xRotation, yRotation + rotationOffset, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation + rotationOffset, 0);
        }
    }

    private void TryJump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            // If the ray hits something within a distance of 1.1f, the player is grounded
            if (hit.distance < 1f)
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
            PausePopUp.transform.SetAsLastSibling();
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;

            PausePopUp.SetActive(true);
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

    public void OnRotated()
    {
        print("ROTATION STATION");
        rotationOffset += 90;
    }

}
