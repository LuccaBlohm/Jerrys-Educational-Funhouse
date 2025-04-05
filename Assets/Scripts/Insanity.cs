using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insanity : MonoBehaviour
{
    public Transform playerCam;
    public Transform portal;
    public Transform otherPortal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 playerOffSetFromPortal = playerCam.position - otherPortal.position;
        //transform.position = portal.position + playerOffSetFromPortal;

        float angularDifference = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifference, Vector3.up);
        Vector3 newCameraDirection = portalRotationDifference * playerCam.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
