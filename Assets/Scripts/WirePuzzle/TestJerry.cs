using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJerry : MonoBehaviour
{
    public Animator jerryAnim;
    public GameObject statueRoomLights;

    public Transform lookTarget;
    public Transform playerTransform;
    public float rotationSpeed = 5f; // 0 instant, 1-10 for smooth

    public bool shouldRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        statueRoomLights = GameObject.Find("StatueRoomAfterWirePuzzle");
    }

    public void activation()
    {
        StartCoroutine(lightEffects());
    }

    private IEnumerator lightEffects()
    {
        shouldRotate = true;
        yield return new WaitForSeconds(0.2f);
        statueRoomLights.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        statueRoomLights.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        statueRoomLights.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        statueRoomLights.SetActive(false);
        yield return new WaitForSeconds(1.4f);
        statueRoomLights.SetActive(true);
        jerryAnim.Play("JerryAnim");
        StopAllCoroutines();
        yield return new WaitForSeconds(5f);
        shouldRotate = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldRotate)
        {
            Vector3 direction = lookTarget.position - playerTransform.position;
            direction.y = 0f; // Ignore vertical difference
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
