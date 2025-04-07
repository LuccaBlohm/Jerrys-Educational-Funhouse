using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] private Transform playerTransform;

    private void OnMouseOver()
    {
        if (range >= distanceFromPlayer())
        {
            CursorBehavior.objectClickable = true;
        }
        else
        {
            CursorBehavior.objectClickable = false;
        }
    }

    protected float distanceFromPlayer()
    {
        return Mathf.Sqrt(Mathf.Pow(transform.position.x - playerTransform.position.x, 2) +
                          Mathf.Pow(transform.position.z - playerTransform.position.z, 2));
    }

    private void OnMouseExit()
    {
        CursorBehavior.objectClickable = false;
    }
}
