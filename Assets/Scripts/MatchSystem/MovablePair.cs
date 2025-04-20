using UnityEngine;

public class MovablePair : MonoBehaviour
{
    private Camera _mainCamera;
    private float _cameraZDistance;
    private Vector3 _initialPositionl;
    private bool _connectied;

    private const string _portTag = "Port";
    private const float _dragResonseThreshold = 2;

    void Start()
    {
        _mainCamera = Camera.main;
        _cameraZDistance = _mainCamera.WorldToScreenPoint(transform.position).z;
        //z axis of the game object for screen view
    }

    private void OnMouseDrag()
    {
        Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cameraZDistance);
        //z axis added to screen point
        Vector3 NewWorldPosition = _mainCamera.ScreenToWorldPoint(ScreenPosition);
        //Screen point converted to world point

        if (!_connectied)
        {
            transform.position = NewWorldPosition;
        }
        else if (Vector3.Distance(a: transform.position, b: NewWorldPosition) < _dragResonseThreshold)
        {
            _connectied = false;
        }
    }

    private void OnMouseUp()
    {
        if (!_connectied)
        {
            ResetPosition();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetInitialPosition(Vector3 NewPosition)
    {
        _initialPositionl = NewPosition;
        transform.position = _initialPositionl;
    }

    private void ResetPosition()
    {
        transform.position = _initialPositionl;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_portTag))
        {
            _connectied = true;
            transform.position = other.transform.position;
        }
    }
}