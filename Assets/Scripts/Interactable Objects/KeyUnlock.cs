using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUnlock : MonoBehaviour, IInteractable
{
    public Animator animator;
    private bool isOpen = false;
    [SerializeField] private ItemSprite key;
    [SerializeField] private PlayerMovement player;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnInteract()
    {
        isOpen = !isOpen; //door open logic, move into the key check once configured in main
        animator.SetBool("Open", isOpen);
        if (key == player.itemHeld)
        {
            Debug.Log("Unlock");

        }
    }


}
