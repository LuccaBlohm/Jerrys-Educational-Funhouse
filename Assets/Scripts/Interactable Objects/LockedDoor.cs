using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable, IPopUpSpawner
{
    [SerializeField] protected GameObject popup;
    [SerializeField] protected Transform canvasTransform;
    [SerializeField] LockScript lockPopUpConnection;
    [SerializeField] int[] lockCombo;

    bool popUpOn;

    public void OnInteract()
    {
        if (!popUpOn)
        {
            lockPopUpConnection = Instantiate(popup, new Vector2(Random.Range(0, 800),
                                                                Random.Range(0, 400)),
                                    Quaternion.identity,
                                    canvasTransform).GetComponent<LockScript>();


            lockPopUpConnection.ConnectToOrigin(gameObject);
            lockPopUpConnection.PassLockCombo(lockCombo);
            popUpOn = true;
        }
    }

    public void ComboCorrect()
    {
        Destroy(gameObject);
    }

    // allows object to be clicked again to spawn another pop up
    public void DisconnectPopUp()
    {
        popUpOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
