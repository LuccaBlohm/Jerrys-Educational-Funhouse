using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;

public class GlobalTimer : MonoBehaviour
{
    float time = 0;
    public GameObject[] levers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 10)
        {
            time = 0;
            resetLevers();
        }
    }
    void resetLevers()
    {
        Debug.Log("resetting levers");
        foreach (GameObject lever in levers)
        {
            RefreshCollider(lever.GetComponent<Collider>());
        }
    }

    void RefreshCollider(Collider col)  //forcing unity to recalculate the collider's physics because apparently its lazy and re-enabling doesnt change anything
    // So as a i understand it, a tree is used for raycasts, lost colliders are removed from the tree but not re-added even when enabled until we do this
    {
        // Forces an actual transform update Unity can't optimize away
        col.transform.localScale *= 1.0001f;
        col.transform.localScale *= 0.9999f;

        // Temporarily disables/enables again for good measure
        col.enabled = false;
        col.enabled = true;
    }
}
