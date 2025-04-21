using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool IsLeftWire;
    [SerializeField] private Color CustomColor;
    private Image _image;
    // private LineRenderer _lineRenderer;
    private Canvas _canvas;
    private bool _isDragStarted = false;
    private WireTask _wireTask;
    public bool IsSuccess = false;

    private RectTransform _rectTransform;
    private RectTransform child;
    private Image childImage;

    private void Awake()
    {
        _image = GetComponent<Image>();
        // _lineRenderer = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();
        _wireTask = GetComponentInParent<WireTask>();

        if (transform.childCount != 0)
        {
            Transform temp = transform.GetChild(0);
            child = temp.GetComponent<RectTransform>();
            childImage = temp.GetComponent<Image>();
        }
        _rectTransform = GetComponentInParent<RectTransform>();
    }

    public void SetColor(Color color)
    {
        _image.color = color;  

        /*
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;*/

        CustomColor = color;

        if (childImage != null)
        {
            childImage.color = color;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragStarted)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
                Input.mousePosition, _canvas.worldCamera, out movePos);

            /*
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _canvas.transform.TransformPoint(movePos));*/

            drawLine(_rectTransform.position, eventData.position);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsLeftWire)
        {
            return;
        }
        if (IsSuccess)
        {
            return;
        }
        _isDragStarted = true;
        _wireTask.CurrentDraggedWire = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_wireTask.CurrentHoveredWire != null)
        {
            if (_wireTask.CurrentHoveredWire.CustomColor == CustomColor && !_wireTask.CurrentHoveredWire.IsLeftWire)
            {
                IsSuccess = true;
                _wireTask.CurrentHoveredWire.IsSuccess = true;
            }
        }

        if (!IsSuccess)
        {
            /*
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);*/

            drawLine(Vector2.zero, Vector2.zero);
        }

        _isDragStarted = false;
        _wireTask.CurrentDraggedWire = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _wireTask.CurrentHoveredWire = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _wireTask.CurrentHoveredWire = null;
    }

    private void drawLine(Vector2 startPoint, Vector2 endPoint)
    {

        if (child != null)
        {
            Vector2 midpoint = (startPoint + endPoint) / 2f;

            child.position = midpoint;

            Vector2 dir = startPoint - endPoint;
            child.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            child.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dir.magnitude/(_canvas.scaleFactor*2));
        }

    }
}