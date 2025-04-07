using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporterScript : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool playerOverlap = false;

    // Update is called once per frame
    void Update()
    {
        if(playerOverlap)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            
            if(dotProduct < 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;
                playerOverlap = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Entered: " + gameObject.name);
        if (other.tag == "Player")
        {
            playerOverlap = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("Exited: " + gameObject.name);
        if (other.tag == "Player")
        {
            playerOverlap = false;
        }
    }
}
