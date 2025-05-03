using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralTrigger : MonoBehaviour
{
    [SerializeField] private GameObject jerry;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Unleashed Jerry");
        jerry.SetActive(true);

    }
}
