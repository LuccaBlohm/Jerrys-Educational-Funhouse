using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUnlock : MonoBehaviour, IInteractable
{
    public Animator animator;
    private bool isOpen = false;
    [SerializeField] private ItemSprite key;
    [SerializeField] private PlayerMovement player;

    public GameObject FinalExit;

    private void Start()
    {
        animator = GetComponent<Animator>();
        FinalExit.SetActive(false);
    }

    public void OnInteract()
    {
        isOpen = !isOpen; //door open logic, move into the key check once configured in main
        animator.SetBool("Open", isOpen);
        StartCoroutine(WaitAndActivateExit());
        if (key == player.itemHeld)
        {
            Debug.Log("Unlock");
        }
    }

    IEnumerator WaitAndActivateExit()
    {
        Debug.Log("Coroutine started!");
        yield return new WaitForSeconds(0.6f);
        FinalExit.SetActive(true);
    }


}
