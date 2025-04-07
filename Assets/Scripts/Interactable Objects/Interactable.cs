using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private void OnMouseOver()
    {
        CursorBehavior.objectClickable = true;
    }

    private void OnMouseExit()
    {
        CursorBehavior.objectClickable = false;
    }
}
