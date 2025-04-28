using System.Collections;
using UnityEngine;

public class KeyUnlock : ItemInteract
{
    public Animator animator;
    private bool isOpen = false;

    public GameObject FinalExit;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (FinalExit != null)
        {
            FinalExit.SetActive(false);
        }

    }

    //  the OnInteract() should be on parent class
    /*
    public void OnInteract()
    {
        if (key == player.itemHeld)
        {
            Debug.Log("Unlock");
            isOpen = !isOpen; //door open logic, move into the key check once configured in main
            animator.SetBool("Open", isOpen);
            StartCoroutine(WaitAndActivateExit());
        }
    }*/

    protected override void interactableBehavior()
    {
        Debug.Log("Unlock");
        isOpen = !isOpen; //door open logic, move into the key check once configured in main
        animator.SetBool("Open", isOpen);
        if (FinalExit != null)
        {
            StartCoroutine(WaitAndActivateExit());
        }
    }

    IEnumerator WaitAndActivateExit()
    {
        Debug.Log("Coroutine started!");
        yield return new WaitForSeconds(0.6f);
        FinalExit.SetActive(true);
    }
}