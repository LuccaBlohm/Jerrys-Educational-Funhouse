using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Interactable
{
    [SerializeField] private GameObject popup;
    [SerializeField] private Transform canvasTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // bug: this ignores ui over it
    // also range goes off canvas
    private void OnMouseDown()
    {
        Instantiate(popup, new Vector2(Random.Range(0, 1000), Random.Range(0, 1000)),
                    Quaternion.identity,
                    canvasTransform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
