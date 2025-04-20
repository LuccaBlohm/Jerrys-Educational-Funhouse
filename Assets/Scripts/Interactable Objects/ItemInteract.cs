using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSprite key;
    [SerializeField] private PlayerMovement player;

    public void OnInteract()
    {
        if (key == player.itemHeld)
        {
            interactableBehavior();
        }
    }

    protected virtual void interactableBehavior()
    {
        Destroy(gameObject);
        Debug.Log("Interacted successfully");
    }

}
