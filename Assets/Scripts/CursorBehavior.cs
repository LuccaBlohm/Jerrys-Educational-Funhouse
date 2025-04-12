using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CursorBehavior : MonoBehaviour, IPointerMoveHandler,
                                             IPointerEnterHandler,
                                             IPointerExitHandler
{
    // Put this script on the canvas of the scene
    // this should be organized better into a state machine
    // but i don't feel like fixing it right now
    PlayerInput pi;
    InputAction cursorLockToggle;

    string resizePoint;
    static public bool objectClickable;

    static public bool overUI;

    [SerializeField] Texture2D[] cursors;

    /* enum below should match
     * array order:
     * pointer = 0
     * click = 1
     * vertical = 2
     * horiztonal = 3
     * diagonal1 = 4
     * diagonal2 = 5
     * eye = 6
     */

    private enum cursorSelect
    {
        pointer,
        click,
        vertical,
        horizontal,
        diagonal1,
        diagonal2,
        eye
    }

    private cursorSelect cursorState;

    [SerializeField] Vector2 hotspot;
    // Start is called before the first frame update
    void Start()
    {
        pi = GetComponent<PlayerInput>();
        cursorLockToggle = pi.currentActionMap.FindAction("CursorLockToggle");
        cursorLockToggle.performed += ctx => cursorLock();

        Cursor.SetCursor(cursors[(int)cursorSelect.pointer], hotspot, CursorMode.Auto);

        cursorState = cursorSelect.pointer;
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

    private void FixedUpdate()
    {
        Cursor.visible = true;

        if (!overUI)
        {
            if (objectClickable)
            {
                manageCursorState(cursorSelect.click);
            }
            else if(Cursor.lockState == CursorLockMode.Locked)
            {
                manageCursorState(cursorSelect.eye);
            }
            else
            {
                manageCursorState(cursorSelect.pointer);
            }
        }

    }

    private void manageCursorState(cursorSelect state)
    {

        // state transition
        if (cursorState != state)
        {
            cursorState = state;
            int stateSelect = (int)cursorState;
            switch (cursorState)
            {
                case cursorSelect.pointer:
                case cursorSelect.click:
                    hotspot = new Vector2(8, 0);
                    break;

                case cursorSelect.vertical:
                case cursorSelect.horizontal:
                case cursorSelect.diagonal1:
                case cursorSelect.diagonal2:
                    hotspot = new Vector2(18, 18);
                    break;

                case cursorSelect.eye:
                    hotspot = new Vector2 (16, 16);
                    break;
            }

            Cursor.SetCursor(cursors[stateSelect], hotspot, CursorMode.Auto);

        }

    }

    private void selectResizeDirection(PointerEventData eventData)
    {
        hotspot = new Vector2(18, 18);
        resizePoint = eventData.pointerEnter.ToString();
        resizePoint = resizePoint.Substring(0, resizePoint.Length - 25);

        switch (resizePoint)
        {
            case "top resize":
            case "bottom":
                manageCursorState(cursorSelect.vertical);
                break;

            case "left":
            case "right":
                manageCursorState(cursorSelect.horizontal);
                break;

            case "top right corner":
            case "bottom left":
                manageCursorState(cursorSelect.diagonal1);
                break;

            case "top left corner":
            case "bottom right":
                manageCursorState(cursorSelect.diagonal2);
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        overUI = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerEnter != null &&
            !eventData.dragging)
        {
            // state behaviors
            switch (cursorState)
            {
                case cursorSelect.pointer:
                    if (eventData.pointerEnter.tag == "Clickable")
                    {
                        manageCursorState(cursorSelect.click);
                    }
                    else if (eventData.pointerEnter.tag == "Resizable")
                    {
                        selectResizeDirection(eventData);
                    }
                    break;

                case cursorSelect.click:
                    if (eventData.pointerEnter.tag != "Clickable")
                    {
                        manageCursorState(cursorSelect.pointer);
                    }
                    break;

                case cursorSelect.vertical:
                case cursorSelect.horizontal:
                case cursorSelect.diagonal1:
                case cursorSelect.diagonal2:

                    
                    selectResizeDirection(eventData);

                    if (eventData.pointerEnter.tag != "Resizable")
                    {
                        manageCursorState(cursorSelect.pointer);
                    }
                    
                    break;
                case cursorSelect.eye:
                    if (Cursor.lockState != CursorLockMode.Locked)
                    {
                        manageCursorState(cursorSelect.pointer);
                    }
                    break;
            }
        }



        /*
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
                        Cursor.SetCursor(cursors[(int)cursorSelect.vertical], hotspot, CursorMode.Auto);
                        break;

                    case "left":
                    case "right":
                        Cursor.SetCursor(cursors[(int)cursorSelect.horizontal], hotspot, CursorMode.Auto);
                        break;

                    case "top right corner":
                    case "bottom left":
                        Cursor.SetCursor(cursors[(int)cursorSelect.diagonal1], hotspot, CursorMode.Auto);
                        break;

                    case "top left corner":
                    case "bottom right":
                        Cursor.SetCursor(cursors[(int)cursorSelect.diagonal2], hotspot, CursorMode.Auto);
                        break;
                }
            }
            else if (eventData.pointerEnter.tag == "Clickable" && !Input.GetMouseButton(0))
            {
                Cursor.SetCursor(cursors[(int)cursorSelect.click], hotspot, CursorMode.Auto);
            }
            else if (!Input.GetMouseButton(0))
            {
                hotspot = new Vector2(8, 0);
                Cursor.SetCursor(cursors[(int)cursorSelect.pointer], hotspot, CursorMode.Auto);
            }

        }else
        {
            hotspot = new Vector2(8, 0);
            Cursor.SetCursor(cursors[(int)cursorSelect.pointer], hotspot, CursorMode.Auto);
        }*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        overUI = false;
    }
}
