using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPopUpSpawner
{
    // Behavior for managing connections between pop ups and interactables
    public void DisconnectPopUp();
}

public class WindowUIManager : MonoBehaviour, IDragHandler,
                                              IBeginDragHandler, 
                                              IEndDragHandler,
                                              IPointerDownHandler
{
    protected Canvas parent;
    protected RectTransform rectTransform;
    protected bool dragging;
    protected bool resizing;
    protected string resizePoint;
    protected Vector2 resizeDirection;
    [SerializeField] protected Vector2 rectTransformMinSize;
    [SerializeField] protected Vector2 rectTransformMaxSize;

    protected GameObject origin;

    protected Vector2 clickPosition;

    void Awake()
    {
        parent = gameObject.transform.parent.GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    // allows pop up to affect origin interactable object
    public virtual void ConnectToOrigin(GameObject origin)
    {
        this.origin = origin;
    }

    public void CloseWindow()
    {
        if (origin != null)
        {
            IPopUpSpawner spawner = origin.GetComponent<IPopUpSpawner>();

            if (spawner != null)
            {
                spawner.DisconnectPopUp();
            }
        }


        Destroy(gameObject);
    }

    // sends window to front when clicked
    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.transform.SetAsLastSibling();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.rawPointerPress.tag == "Resizable")
        {
            resizing = true;

            resizePoint = eventData.rawPointerPress.ToString();
            resizePoint = resizePoint.Substring(0, resizePoint.Length - 25);

            resizeDirection = new Vector2(1, 1); 

            // checks which corner is being dragged
            // to ensure correct resize direction and behavior
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
        bound xBound = checkXBounds(eventData);
        bound yBound = checkYBounds(eventData);

        if (dragging)
        {
            // prevents from dragging off screen
            keepInBounds(xBound, yBound);

            // moves window
            rectTransform.anchoredPosition += eventData.delta / parent.scaleFactor;

            Debug.Log(xBound +", " + yBound);

        }
        else if (resizing)
        {
            // sets size
            if (xBound == bound.none && yBound == bound.none)
            {
                rectTransform.sizeDelta += (eventData.delta * resizeDirection / 2) / parent.scaleFactor;
            }


            bool xMinPassed = rectTransform.sizeDelta.x < rectTransformMinSize.x;
            bool yMinPassed = rectTransform.sizeDelta.y < rectTransformMinSize.y;

            // sets size limit
            // mild bug: might unintentionally move window slightly if resizing too quickly
            /*switch (xMinPassed, yMinPassed)
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

            // prevents from resizing off screen
            switch (x, y)
            {
                case (bound.right, bound.none):
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                                                            Screen.width - rectTransform.position.x/2);
                    break;

                case (bound.left, bound.none):

                    break;

                case (bound.none, bound.up):

                    break;

                case (bound.none, bound.down):

                    break;

                case (bound.right, bound.up):

                    break;

                case (bound.left, bound.up):

                    break;

                case (bound.right, bound.down):

                    break;

                case (bound.left, bound.down):

                    break;

            }*/

            switch (xMinPassed, yMinPassed, xBound, yBound)
            {
                case (false, false, bound.none, bound.none):
                    rectTransform.anchoredPosition += (eventData.delta *
                                                      new Vector2(Mathf.Abs(resizeDirection.x),
                                                      Mathf.Abs(resizeDirection.y)) / 2)
                                                      / parent.scaleFactor;
                    break;

                case (false, false, bound.right, bound.none):
                case (false, false, bound.left, bound.none):
                    rectTransform.anchoredPosition += (eventData.delta *
                                                      new Vector2(0,
                                                      Mathf.Abs(resizeDirection.y)) / 2)
                                                      / parent.scaleFactor;
                    break;

                case (false, false, bound.none, bound.up):
                case (false, false, bound.none, bound.down):
                    rectTransform.anchoredPosition += (eventData.delta *
                                                      new Vector2(Mathf.Abs(resizeDirection.x),
                                                      0) / 2)
                                                      / parent.scaleFactor;
                    break;

                case (true, false, bound.none, bound.none):
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransformMinSize.x);
                    rectTransform.anchoredPosition += (eventData.delta *
                                  new Vector2(0, Mathf.Abs(resizeDirection.y)) / 2)
                                  / parent.scaleFactor;
                    break;

                case (false, true, bound.none, bound.none):
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTransformMinSize.y);
                    rectTransform.anchoredPosition += (eventData.delta *
                                  new Vector2(Mathf.Abs(resizeDirection.x), 0) / 2)
                                  / parent.scaleFactor;
                    break;

                case (true, true, bound.none, bound.none):
                    rectTransform.sizeDelta = rectTransformMinSize;
                    break;
            }

            keepInBounds(xBound, yBound);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        resizing = false;
    }

    private enum bound
    {
        none,
        up,
        down,
        left,
        right
    }

    private bound checkXBounds(PointerEventData eventData)
    {
        bool rightBound = eventData.delta.x + rectTransform.position.x + rectTransform.sizeDelta.x / 2 >= Screen.width;

        bool leftBound = eventData.delta.x + rectTransform.position.x - rectTransform.sizeDelta.x / 2 <= 0;

        if (rightBound)
        {
            return bound.right;
        }
        else if (leftBound)
        {
            return bound.left;
        }

        return bound.none;
    }

    private bound checkYBounds(PointerEventData eventData)
    {
        bool upBound = eventData.delta.y + rectTransform.position.y + rectTransform.sizeDelta.y / 2 >= Screen.height;

        bool downBound = eventData.delta.y + rectTransform.position.y - rectTransform.sizeDelta.y / 2 <= 0;

        if (upBound)
        {
            return bound.up;
        }
        else if (downBound)
        {
            return bound.down;
        }

        return bound.none;
    }

    private void keepInBounds(bound x, bound y)
    {
        switch (x, y)
        {
            case (bound.right, bound.none):
                rectTransform.position = new Vector2(Screen.width - rectTransform.sizeDelta.x / 2,
                                                     rectTransform.position.y);
                break;

            case (bound.left, bound.none):
                rectTransform.position = new Vector2(0 + rectTransform.sizeDelta.x / 2,
                                                     rectTransform.position.y);
                break;

            case (bound.none, bound.up):
                rectTransform.position = new Vector2(rectTransform.position.x,
                                                     Screen.height - rectTransform.sizeDelta.y / 2);
                break;

            case (bound.none, bound.down):
                rectTransform.position = new Vector2(rectTransform.position.x,
                                                     0 + rectTransform.sizeDelta.y / 2);
                break;

            case (bound.right, bound.up):
                rectTransform.position = new Vector2(Screen.width - rectTransform.sizeDelta.x / 2,
                                                     Screen.height - rectTransform.sizeDelta.y / 2);
                break;

            case (bound.left, bound.up):
                rectTransform.position = new Vector2(0 + rectTransform.sizeDelta.x / 2,
                                                     Screen.height - rectTransform.sizeDelta.y / 2);
                break;

            case (bound.right, bound.down):
                rectTransform.position = new Vector2(Screen.width - rectTransform.sizeDelta.x / 2,
                                                     0 + rectTransform.sizeDelta.y / 2);
                break;

            case (bound.left, bound.down):
                rectTransform.position = new Vector2(0 + rectTransform.sizeDelta.x / 2,
                                                     0 + rectTransform.sizeDelta.y / 2);
                break;


        }
    }
}
