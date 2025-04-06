using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CursorBehavior : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler
{
    // Put this script on the canvas of the scene

    PlayerInput pi;
    InputAction cursorLockToggle;

    string resizePoint;

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

    public void OnPointerMove(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null)
        {
            // checks if hovering over resizable area
            if (eventData.pointerEnter.tag == "Resizable" && !Input.GetMouseButton(0))
            {
                hotspot = new Vector2(18, 18);
                resizePoint = eventData.pointerEnter.ToString();
                resizePoint = resizePoint.Substring(0, resizePoint.Length - 25);

                switch (resizePoint)
                {
                    case "top resize":
                    case "bottom":
                        Cursor.SetCursor(cursors[2], hotspot, CursorMode.Auto);
                        break;

                    case "left":
                    case "right":
                        Cursor.SetCursor(cursors[3], hotspot, CursorMode.Auto);
                        break;

                    case "top right corner":
                    case "bottom left":
                        Cursor.SetCursor(cursors[4], hotspot, CursorMode.Auto);
                        break;

                    case "top left corner":
                    case "bottom right":
                        Cursor.SetCursor(cursors[5], hotspot, CursorMode.Auto);
                        break;
                }
            }
            else if (eventData.pointerEnter.tag == "Clickable")
            {
                Cursor.SetCursor(cursors[1], hotspot, CursorMode.Auto);
            }
            else if (!Input.GetMouseButton(0))
            {
                hotspot = new Vector2(8, 0);
                Cursor.SetCursor(cursors[0], hotspot, CursorMode.Auto);
            }

        }else
        {
            hotspot = new Vector2(8, 0);
            Cursor.SetCursor(cursors[0], hotspot, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(0))
        {
            hotspot = new Vector2(8, 0);
            Cursor.SetCursor(cursors[0], hotspot, CursorMode.Auto);
        }

    }
}
