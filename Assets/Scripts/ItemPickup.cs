using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Transform playerCamera, container;
    [SerializeField] private LayerMask objectLayer;
    [SerializeField] private PlayerInput pI;
    [SerializeField] private float maxDis;

    [SerializeField] private GameObject parent, child;

    private InputAction _pickUp;
    private InputAction _throw;

    private MeshCollider objColl;
    private Rigidbody objRb;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform releasePosition;

    [SerializeField]
    [Range(10, 100)]
    private int linePoints = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float timeBetweenPoints = 0.1f;

    private int defaultScale;

    void Start()
    {
        defaultScale = 2;
        pI = GetComponent<PlayerInput>();
        _pickUp = pI.currentActionMap.FindAction("Pickup");
    }

    private void PickUp(RaycastHit hit)
    {

        hit.transform.SetParent(gameObject.transform);

        child = parent.transform.GetChild(0).gameObject;

        objColl = child.transform.GetComponent<MeshCollider>();
        objRb = child.transform.GetComponent<Rigidbody>();

        child.transform.localPosition = Vector3.zero;
        child.transform.localRotation = Quaternion.Euler(Vector3.zero);
        child.transform.localScale = Vector3.one;

        objRb.isKinematic = true;
        objColl.isTrigger = true;
    }

    void Update()
    {
        if (_pickUp.IsPressed())
        {
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit raycastHit, maxDis, objectLayer))
            {
                PickUp(raycastHit);
            }
        }
    }
}
