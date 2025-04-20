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
            Destroy(gameObject);
        }

    }

}
