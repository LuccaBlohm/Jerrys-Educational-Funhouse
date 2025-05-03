using UnityEngine;

public class ItemInteract : MonoBehaviour, IInteractable, IPopUpSpawner
{
    [SerializeField] private ItemSprite key;
    [SerializeField] private PlayerMovement player;
    [SerializeField] protected AudioSource _rejectSound;
    [SerializeField] protected AudioSource _acceptSound;

    [SerializeField] protected WindowUIManager popUpConnection;
    [SerializeField] protected GameObject errorPopUp;
    [SerializeField] protected RectTransform canvasTransform;
    private Canvas popUpCanvas;
    private bool popUpOn;

    public void OnInteract()
    {
        if (key == player.itemHeld)
        {
            if (_acceptSound != null)
            {
                _acceptSound.Play();
                _acceptSound = null;
                _rejectSound = null;
            }

            interactableBehavior();
        }
        else
        {
            if(_rejectSound != null)
            {
                _rejectSound.Play();
            }

            if (!popUpOn && errorPopUp != null && popUpConnection == null && canvasTransform != null)
            {
                popUpCanvas = canvasTransform.GetComponent<Canvas>();
                popUpConnection = Instantiate(errorPopUp, new Vector2(popUpCanvas.renderingDisplaySize.x/2,
                                                                      popUpCanvas.renderingDisplaySize.y/2),
                        Quaternion.identity,
                        canvasTransform).GetComponent<WindowUIManager>();

                popUpConnection.ConnectToOrigin(gameObject);

                popUpOn = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    protected virtual void interactableBehavior()
    {
        Destroy(gameObject);
        Debug.Log("Interacted successfully");
    }

    public void DisconnectPopUp()
    {
        popUpOn = false;
    }
}