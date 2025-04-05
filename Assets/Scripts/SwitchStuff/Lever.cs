using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    void OnInteract();
}

public class Lever : MonoBehaviour, IInteractable
{
    public string switchNum; //to be set in inspector, will be I, II, III, or IV
    public GameObject switchManager;
    public void Start()
    {
        switchManager = GameObject.Find("SwitchManager");
    }
    public void OnInteract()
    {
        switchManager.GetComponent<SwitchManager>().LeverFlip(switchNum);
    }
}
