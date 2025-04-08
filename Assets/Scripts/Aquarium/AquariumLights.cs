using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumLights : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(10f, 20f, 0f);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
    }
}
