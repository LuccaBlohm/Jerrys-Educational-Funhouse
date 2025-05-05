using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteHallwayTeleport : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = 34;   
    }

    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 
            player.transform.position.z + offset);
    }
}
