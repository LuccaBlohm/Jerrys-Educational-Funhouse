using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralJerry : MonoBehaviour
{

    public GameObject player;
    public float speed = 2f;

    public float teleportDistance = 25f;
    public float teleportThreshold = 40f;

    public float TPcooldown = 0f;
    public Vector3 localPosition;

    [SerializeField] private AudioSource breezeWoosh;
    [SerializeField] private AudioSource jumpScareAudio;

    private bool breezeOnCooldown = false;
    private bool scarePlayed = false;

    public float dist;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        localPosition = player.transform.position - transform.position;

        transform.LookAt(player.transform.position, Vector3.up);//rotate to face player

        dist = Vector3.Distance(player.transform.position, transform.position);

        if (!breezeOnCooldown && dist > 12 && dist < 13)
        {
            StartCoroutine(breeze()); //breeze woosh
        }
        if (!scarePlayed && dist < 0.05f)
        {
            StartCoroutine(jumpScare());//play jumpscare
        }
        if (localPosition.magnitude >= teleportThreshold && TPcooldown > 10f)
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            Vector3 offset = Vector3.up * 0.5f + Vector3.right * Random.Range(-1f, 1f);
            transform.position = player.transform.position - directionToPlayer * teleportDistance + offset;
            TPcooldown = 0;
        }

        localPosition = localPosition.normalized; // The normalized direction in LOCAL space
        localPosition.y += 0.5f;


        transform.Translate(localPosition.x * Time.deltaTime * speed, localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);

        TPcooldown += Time.deltaTime;
    }

    IEnumerator breeze()
    {
        breezeOnCooldown = true;
        breezeWoosh.Play();
        yield return new WaitForSeconds(20f);
        breezeOnCooldown = false;
    }

    IEnumerator jumpScare()
    {
        scarePlayed = true;
        jumpScareAudio.Play();
        yield return new WaitForSeconds(15f);
        scarePlayed = false;
    }


}
