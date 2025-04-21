using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpPopUp : MonoBehaviour, IInteractable, IPopUpSpawner
{
    [SerializeField] private GameObject popUp;
    [SerializeField] private WindowUIManager popUpConnection;
    [SerializeField] private Transform canvasTransform;
    bool popUpOn;


    public void OnInteract()
    {
        if (!popUpOn)
        {
            popUpConnection = Instantiate(popUp, new Vector2(Random.Range(0, 800),
                                                                Random.Range(0, 400)),
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
