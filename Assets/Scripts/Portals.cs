using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public Transform playerCam;
    public Transform portal;
    public Transform otherPortal;

    public int sidePortal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (sidePortal == 1)
        {
            Vector3 playerOffSetFromPortal = playerCam.position - otherPortal.position;
            Vector3 sidePortal = new Vector3(playerOffSetFromPortal.z, playerOffSetFromPortal.y, -playerOffSetFromPortal.x);
            transform.position = portal.position + sidePortal;

            float angularDifference = Quaternion.Angle(portal.rotation, otherPortal.rotation);

            Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifference, Vector3.up);
            Vector3 newCameraDirection = portalRotationDifference * playerCam.forward;
            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
        else if (sidePortal == 2)
        {
            Vector3 playerOffSetFromPortal = playerCam.position - otherPortal.position;
            Vector3 sidePortal = new Vector3(-playerOffSetFromPortal.z, playerOffSetFromPortal.y, playerOffSetFromPortal.x);
            transform.position = portal.position + sidePortal;

            float angularDifference = Quaternion.Angle(portal.rotation, otherPortal.rotation);

            Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifference, Vector3.up);
            portalRotationDifference.eulerAngles = new Vector3(portalRotationDifference.eulerAngles.x, portalRotationDifference.eulerAngles.y + 180, portalRotationDifference.eulerAngles.z);
            Vector3 newCameraDirection = portalRotationDifference * playerCam.forward;
            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
        else if (sidePortal == 0)
        {
            Vector3 playerOffSetFromPortal = playerCam.position - otherPortal.position;
            transform.position = portal.position + playerOffSetFromPortal;

            float angularDifference = Quaternion.Angle(portal.rotation, otherPortal.rotation);

            Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifference, Vector3.up);
            Vector3 newCameraDirection = portalRotationDifference * playerCam.forward;
            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
        else
        {
            Vector3 playerOffSetFromPortal = playerCam.position - otherPortal.position;
            transform.position = portal.position + playerOffSetFromPortal;

            float angularDifference = Quaternion.Angle(portal.rotation, otherPortal.rotation);

            Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifference, Vector3.up);
            portalRotationDifference.eulerAngles = new Vector3(portalRotationDifference.eulerAngles.x, portalRotationDifference.eulerAngles.y + 180, portalRotationDifference.eulerAngles.z);
            Vector3 newCameraDirection = portalRotationDifference * playerCam.forward;
            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
    }
}
