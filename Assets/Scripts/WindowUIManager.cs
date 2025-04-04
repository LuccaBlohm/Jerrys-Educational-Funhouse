using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowUIManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    Canvas parent;
    RectTransform rectTransform;
    bool dragging;
    bool resizing;
    string resizePoint;
    Vector2 resizeDirection;
    [SerializeField] private Vector2 rectTransformMinSize;

    void Awake()
    {
        parent = gameObject.transform.parent.GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void CloseWindow()
    {
        Destroy(gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.transform.SetAsLastSibling();

        if (eventData.rawPointerPress.tag == "Resizable")
        {
            resizing = true;

            resizePoint = eventData.rawPointerPress.ToString();
            resizePoint = resizePoint.Substring(0, resizePoint.Length - 25);

            resizeDirection = new Vector2(1, 1); 

            // checks which corner is being dragged
            switch (resizePoint)
            {
                case "top left corner":
                    resizeDirection = new Vector2(-1, 1);
                    break;

                case "bottom left":
                    resizeDirection = new Vector2(-1, -1);
                    break;

                case "bottom right":
                    resizeDirection = new Vector2(1, -1);
                    break;

                case "left":
                    resizeDirection = new Vector2(-1, 0);
                    break;

                case "right":
                    resizeDirection = new Vector2(1, 0);
                    break;

                case "bottom":
                    resizeDirection = new Vector2(0, -1);
                    break;

                case "top resize":
                    resizeDirection = new Vector2(0, 1);
                    break;
            }


        }
        else if (eventData.rawPointerPress.tag == "Draggable")
        {
            dragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            // moves window
            rectTransform.anchoredPosition += eventData.delta / parent.scaleFactor;
        }else if (resizing)
        {
            // sets size
            rectTransform.sizeDelta += (eventData.delta * resizeDirection / 2) / parent.scaleFactor;

            bool xMinPassed = rectTransform.sizeDelta.x < rectTransformMinSize.x;
            bool yMinPassed = rectTransform.sizeDelta.y < rectTransformMinSize.y;

            // sets size limit
            // mild bug: might unintentionally move window slightly if resizing too quickly
            switch (xMinPassed, yMinPassed)
            {
                case (false, false):
                    rectTransform.anchoredPosition += (eventData.delta *
                                  new Vector2(Mathf.Abs(resizeDirection.x),
                                  Mathf.Abs(resizeDirection.y)) / 2)
                                  / parent.scaleFactor;
                    break;

                case (true, false):
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransformMinSize.x);
                    rectTransform.anchoredPosition += (eventData.delta *
                                  new Vector2(0, Mathf.Abs(resizeDirection.y)) / 2)
                                  / parent.scaleFactor;
                    break;

                case (false, true):
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTransformMinSize.y);
                    rectTransform.anchoredPosition += (eventData.delta *
                                  new Vector2(Mathf.Abs(resizeDirection.x), 0) / 2)
                                  / parent.scaleFactor;
                    break;

                case (true, true):
                    rectTransform.sizeDelta = rectTransformMinSize;
                    break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        resizing = false;
    }
}
