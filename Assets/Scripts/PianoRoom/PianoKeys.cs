using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKeys : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 downPosition;
    private Vector3 targetPosition;
    private bool isPressed = false;
    private bool hasPlayed = false;
    public string keyLetter;
    private PianoPuzzle pp;
    private AudioSource audioSource;
    private bool keyBuffer = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        originalPosition = transform.localPosition;
        downPosition = transform.localPosition - new Vector3(0, 0.04f, 0);
        pp = GameObject.Find("PianoPuzzleManager").GetComponent<PianoPuzzle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            targetPosition = downPosition;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * 15f);


            if (!keyBuffer && !hasPlayed && Vector3.Distance(transform.localPosition, targetPosition) < 0.01f)
            {
                audioSource.Play();
                hasPlayed = true;
                pp.RegisterKeyPress(keyLetter);
                keyBuffer = true;
                StartCoroutine(KeyPressGap());
            }
        }

        if (!isPressed)
        {
            targetPosition = originalPosition;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * 15f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPressed = true;
            hasPlayed = false;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPressed = false;
        }
    }

    IEnumerator KeyPressGap()
    {
        yield return new WaitForSeconds(.4f);
        keyBuffer = false;
    }
}
