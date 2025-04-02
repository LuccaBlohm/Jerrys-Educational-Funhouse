using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    [SerializeField]
    [Range(0, 9)]
    private int numOne, numTwo, numThree;

    [SerializeField] private int correctOne, correctTwo, correctThree;
    [SerializeField] private TMP_Text numTextOne, numTextTwo, numTextThree;

    void Start()
    {
        
    }



    void Update()
    {
        
    }
}
