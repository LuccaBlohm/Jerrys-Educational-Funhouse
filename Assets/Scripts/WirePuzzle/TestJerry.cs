using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJerry : MonoBehaviour
{
    public Animator jerryAnim;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void activation()
    {
        jerryAnim.Play("JerryAnim");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
