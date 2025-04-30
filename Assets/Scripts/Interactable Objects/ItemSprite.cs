using UnityEngine;
using UnityEngine.UI;

public class ItemSprite : MonoBehaviour, IInteractable
{

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject itemCanvas;
    [SerializeField] private RawImage itemVisual;
    [SerializeField] private Texture itemTexture;

    [SerializeField] private AudioSource _dropAudioSource;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        itemCanvas = playerTransform.GetChild(2).gameObject;
        itemVisual = itemCanvas.transform.GetChild(0).GetComponent<RawImage>();
        _dropAudioSource.Pause();
    }

    public void OnInteract()
    {
        gameObject.SetActive(false);

        itemVisual.texture = itemTexture;
        itemCanvas.SetActive(true);
        _dropAudioSource.Play();
    }

    public void AttachToPlayer(Transform player)
    {
        if (playerTransform == null)
        {
            playerTransform = player;
        }

        RawImage itemHolder = player.GetComponentInChildren<RawImage>();

        if (itemHolder != null)
        {
            itemVisual = itemHolder;
        }
    }

    public void Drop()
    {
        itemCanvas.SetActive(false);
        transform.position = playerTransform.position;
        gameObject.SetActive(true);
        _dropAudioSource.Play();
    }

    private void FixedUpdate()
    {
        // sprite always faces camera direction
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }
}