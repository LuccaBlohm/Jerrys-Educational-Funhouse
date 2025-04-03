using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowUIManager : MonoBehaviour, IDragHandler, IBeginDragHandler
{

    Canvas parent;
    RectTransform rectTransform;

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
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / parent.scaleFactor;
    }
}
