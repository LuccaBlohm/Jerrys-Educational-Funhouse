using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    void OnInteract();
}

public class Lever : MonoBehaviour, IInteractable
{
    public bool leverPos = false; //false for down, true for up, down has z rotation of 40 and up has z rotation of -40
    public string switchNum; //to be set in inspector, will be I, II, III, or IV
    public GameObject switchManager;
    public void Start()
    {
        switchManager = GameObject.Find("SwitchManager");
    }
    public void OnInteract()
    {
        switchManager.GetComponent<SwitchManager>().LeverFlip(switchNum);
        SwitchPosition();
    }

    public void SwitchPosition()
    {
        leverPos = !leverPos;

        Vector3 currentEuler = transform.rotation.eulerAngles;
        float newZ = leverPos ? -40f : 40f;
        transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, newZ);
    }
}
