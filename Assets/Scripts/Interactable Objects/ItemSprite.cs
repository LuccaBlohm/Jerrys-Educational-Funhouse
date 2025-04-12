using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprite : MonoBehaviour, IInteractable
{

    [SerializeField] private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    public void OnInteract()
    {

    }

    private void FixedUpdate()
    {
        // sprite always faces camera direction
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }
}
