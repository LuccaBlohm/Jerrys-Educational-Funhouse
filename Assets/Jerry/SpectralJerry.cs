using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralJerry : ItemInteract
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
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.1f); //use scare range from the !scareplayed if statement
        }

        localPosition = player.transform.position - transform.position;

        transform.LookAt(player.transform.position, Vector3.up);//rotate to face player

        dist = Vector3.Distance(player.transform.position, transform.position);

        if (!breezeOnCooldown && dist > 12 && dist < 13)
        {
            StartCoroutine(breeze()); //breeze woosh
        }

        if (!scarePlayed && dist < 2f)
        {
            StartCoroutine(jumpScare());//play jumpscare
        }

        if (localPosition.magnitude >= teleportThreshold && TPcooldown > 10f)
        {
            teleport();
        }

        localPosition = localPosition.normalized; // The normalized direction in LOCAL space
        localPosition.y += 0.5f;


        transform.Translate(localPosition.x * Time.deltaTime * speed, localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);

        TPcooldown += Time.deltaTime;
    }

    private void teleport()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Vector3 offset = Vector3.up * 0.5f + Vector3.right * Random.Range(-1f, 1f);
        transform.position = player.transform.position - directionToPlayer * teleportDistance + offset;
        TPcooldown = 0;
    }

    protected override void interactableBehavior()
    {
        teleport();
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
        Debug.Log("JumpScare triggered at dist: " + dist);
        scarePlayed = true;
        jumpScareAudio.Play();
        StartCoroutine(ShakeCamera());
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(5f); //TIME MOVES SLOW FOR A LIL BIT :)
        Time.timeScale = 1f;
        yield return new WaitForSeconds(5f);

        //Tp JERRY AGAIN    
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Vector3 offset = Vector3.up * 0.5f + Vector3.right * Random.Range(-1f, 1f);
        transform.position = player.transform.position - directionToPlayer * teleportDistance + offset;
        TPcooldown = 0;

        scarePlayed = false;
        gameObject.SetActive(false);


    }


    public float duration = 2f;
    public float magnitude = 0.5f;

    public Transform cameraTransform;

    IEnumerator ShakeCamera()
    {
        if (cameraTransform == null)
        {
            Debug.LogWarning("Camera Transform not assigned");
            yield break;
        }

        Debug.Log("shake");
        Vector3 originalPos = cameraTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cameraTransform.localPosition = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPos;
    }


}
