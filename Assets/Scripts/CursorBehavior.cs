using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CursorBehavior : MonoBehaviour, IPointerEnterHandler
{
    // Put this script on the canvas of the scene

    PlayerInput pi;
    InputAction cursorLockToggle;

    [SerializeField] Texture2D[] cursors;
    int cursorSelector;
    /* array order:
     * pointer = 0
     * click = 1
     * vertical = 2
     * horiztonal = 3
     * diagonal1 = 4
     * diagonal2 = 5
     */

    [SerializeField] Vector2 hotspot;
    // Start is called before the first frame update
    void Start()
    {
        pi = GetComponent<PlayerInput>();
        cursorLockToggle = pi.currentActionMap.FindAction("CursorLockToggle");

        cursorSelector = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(cursors[0], hotspot, CursorMode.Auto);
    }

    private void mouseState()
    {

    }

    private void cursorLock()
    {
        if (cursorLockToggle.WasPressedThisFrame() && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (cursorLockToggle.WasPressedThisFrame())
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;

        cursorLock();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
}
