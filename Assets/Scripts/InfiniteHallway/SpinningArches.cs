using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningArches : MonoBehaviour
{
    private Animator archController;

    // Start is called before the first frame update
    void Start()
    {
        archController = gameObject.GetComponent<Animator>();
        archController.Play("Spin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
