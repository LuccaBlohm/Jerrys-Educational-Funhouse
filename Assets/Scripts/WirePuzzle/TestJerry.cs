using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJerry : MonoBehaviour
{
    public Animator jerryAnim;
    public GameObject statueRoomLights;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
