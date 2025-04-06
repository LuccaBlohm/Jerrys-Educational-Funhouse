using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ChalkboardTrigger : MonoBehaviour
{
    public int roomVisitCount = 0;
    public TextMeshPro chalkboardTextMesh; //gonna assigne in inspector
    public string[] chalkboardText = new string[] { "What", "is", "brown,", "has", "a", "head,", "and", "tails", "but", "no", "legs?" };

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 direction = (other.transform.position - transform.position).normalized;

        // Create a ray from the trigger's position in the calculated direction
        Ray ray = new Ray(transform.position, direction);

        // Perform a raycast
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider == other)
            {
                // Handle the specific direction
                if (direction.z > 0.5f)
                {
                    roomVisitCount++;
                    ChangeChalkboardText();
                    Debug.Log("Entered Classroom from the front side");
                }
                else if (direction.z < -0.5f)
                {
                    roomVisitCount--;
                    Debug.Log("Entered Classroom from the back side");
                }
            }
        }
    }

    public void ChangeChalkboardText()
    {
        ChangeChalkboardText();
        chalkboardTextMesh.text = chalkboardText[roomVisitCount % chalkboardText.Length];

    }

    // Update is called once per frame
    void Update()
    {

    }
}

