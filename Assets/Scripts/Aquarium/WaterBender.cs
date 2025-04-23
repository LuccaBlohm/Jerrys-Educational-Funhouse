using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBender : MonoBehaviour //, IInteractable
{ 
    public Transform player;
    public List<GameObject> cubeList = new List<GameObject>();
    public float baseHeight = 0f;
    public float minHeight = -2f;
    public float effectRadius = 10f;
    public float lerpSpeed = 5f;
    public bool isFrozen = false;

    private PlayerMovement pm;

    [SerializeField] private ItemSprite iceBall;

    /*
    public void OnInteract()
    {
        if (key == pm.itemHeld)
        {
            if (!isFrozen)
            {
                isFrozen = true;
            }
            else
            {
                isFrozen = false;
            }

        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ice Ball")
        {
            isFrozen = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (pm.itemHeld == iceBall)
            {
                isFrozen = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pm = player.GetComponent<PlayerMovement>();

        foreach (Transform child in transform)
        {
            cubeList.Add(child.gameObject);
            child.position = new Vector3(child.position.x, baseHeight, child.position.z);
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (!pm.isInWaterEffectZone)
        {
            isFrozen = false;
        }*/

        foreach (GameObject cube in cubeList)
        {
            float targetY = baseHeight;

            if (pm.isInWaterEffectZone)
            {
                if (isFrozen)
                {
                    continue;
                }

                Vector2 playerXZ = new Vector2(player.position.x, player.position.z);
                Vector2 cubeXZ = new Vector2(cube.transform.position.x, cube.transform.position.z);
                float distance = Vector2.Distance(playerXZ, cubeXZ);
                float ringCenter = 5f;
                float ringWidth = 7f;
                float halfWidth = ringWidth * 0.5f;
                float distanceFromRing = Mathf.Abs(distance - ringCenter);
                float normalized = Mathf.Clamp01(distanceFromRing / halfWidth);
                float dipStrength = 1f - normalized;

                targetY = Mathf.Lerp(baseHeight, minHeight, dipStrength);
            }

            if (!isFrozen)
            {
                Vector3 pos = cube.transform.position;
                pos.y = Mathf.Lerp(pos.y, targetY, Time.deltaTime * lerpSpeed);
                cube.transform.position = pos;
            }
        }
    }
}
