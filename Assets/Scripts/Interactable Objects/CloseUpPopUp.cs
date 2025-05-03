using UnityEngine;

public class CloseUpPopUp : MonoBehaviour, IInteractable, IPopUpSpawner
{
    [SerializeField] private GameObject popUp;
    private RectTransform popUpTransform;
    [SerializeField] private WindowUIManager popUpConnection;
    private Canvas popUpCanvas;
    [SerializeField] private RectTransform canvasTransform;
    bool popUpOn;

    private SpriteRenderer sr;
    private Texture tex;
    [SerializeField] private Vector2 textureSize;

    [SerializeField] private AudioSource _pickupAudio;

    private void Start()
    {
        _pickupAudio.Pause();
        popUpTransform = popUp.GetComponent<RectTransform>();
        popUpCanvas = canvasTransform.GetComponent<Canvas>();
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            tex = sr.sprite.texture;
        }
    }


    public void OnInteract()
    {
        if (!popUpOn && popUpConnection == null)
        {
            popUpConnection = Instantiate(popUp, new Vector2(   Random.Range(popUpTransform.sizeDelta.x/2,
                                                                             popUpCanvas.renderingDisplaySize.x - popUpTransform.sizeDelta.x/2),
                                                                Random.Range(popUpTransform.sizeDelta.y/2,
                                                                             popUpCanvas.renderingDisplaySize.y - 100 - popUpTransform.sizeDelta.y)),
                                    Quaternion.identity,
                                    canvasTransform).GetComponent<WindowUIManager>();

            popUpConnection.addTexture(tex, textureSize);
            popUpConnection.ConnectToOrigin(gameObject);
            popUpOn = true;
            Cursor.lockState = CursorLockMode.Confined;
            _pickupAudio.Play();
        }
    }

    public void DisconnectPopUp()
    {
        popUpOn = false;
    }
}