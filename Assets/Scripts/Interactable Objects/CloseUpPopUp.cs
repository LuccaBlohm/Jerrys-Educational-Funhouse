using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpPopUp : MonoBehaviour, IInteractable, IPopUpSpawner
{
    [SerializeField] private GameObject popUp;
    private RectTransform popUpTransform;
    [SerializeField] private WindowUIManager popUpConnection;
    [SerializeField] private RectTransform canvasTransform;
    bool popUpOn;

    private void Start()
    {
        popUpTransform = popUp.GetComponent<RectTransform>();
    }


    public void OnInteract()
    {
        if (!popUpOn)
        {
            popUpConnection = Instantiate(popUp, new Vector2(   Random.Range(popUpTransform.sizeDelta.x/2,
                                                                             canvasTransform.sizeDelta.x - popUpTransform.sizeDelta.x/2),
                                                                Random.Range(popUpTransform.sizeDelta.y/2,
                                                                             canvasTransform.sizeDelta.y - popUpTransform.sizeDelta.y/2)),
                                    Quaternion.identity,
                                    canvasTransform).GetComponent<WindowUIManager>();


            popUpConnection.ConnectToOrigin(gameObject);
            popUpOn = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void DisconnectPopUp()
    {
        popUpOn = false;
    }
}
