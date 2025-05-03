using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PianoPuzzle : MonoBehaviour
{
    public List<string> puzzleWords = new List<string> { "BED", "FACE", "CAGE", "DEAD" };
    public List<GameObject> spotlights;
    private int currentRound = 0;
    private string currentInput = "";

    public GameObject leverPanel;
    private Vector3 panelEnd;
    public float slideSpeed = .06f;

    public Animator spotlightAnimator;

    private PlayerMovement pm;
    public Transform player;
    public bool ppBegin = false;

    private AudioSource spotlightOn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateSpotlights());
        panelEnd = leverPanel.transform.position - new Vector3(0, 17.33f, 0);
        pm = player.GetComponent<PlayerMovement>();

        spotlightOn = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.pianoPuzzleBegin && !ppBegin)
        {
            ppBegin = true;
            StartCoroutine(TurnOnSpotlightByRound());
        }
    }

    public void RegisterKeyPress(string key)
    {
        currentInput += key.ToUpper();

        if (currentInput.Length >= puzzleWords[currentRound].Length)
        {
            string lastFew = currentInput.Substring(currentInput.Length - puzzleWords[currentRound].Length);
            if (lastFew == puzzleWords[currentRound])
            {
                currentRound++;
                if (currentRound < (puzzleWords.Count))
                {
                    currentInput = "";
                    ppBegin = false;
                    StartCoroutine(ActivateSpotlights());
                }
                else
                {
                    spotlights[3].SetActive(false);
                    StartCoroutine(MoveLeverPanel());
                    StartCoroutine(SpotlightsRotate());
                }
            }
        }
    }

    IEnumerator ActivateSpotlights()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 4; i++)
        {
            spotlights[i].SetActive(false);
        }

        spotlightOn.Play();
        yield return null;
    }

    IEnumerator MoveLeverPanel()
    {
        while (Vector3.Distance(leverPanel.transform.position, panelEnd) > 0.01f)
        {
            leverPanel.transform.position = Vector3.Lerp(leverPanel.transform.position, panelEnd, Time.deltaTime * slideSpeed);
            StartCoroutine(DelayedRefresh(Lever));
            yield return null;
        }
    }

    public GameObject Lever;

    IEnumerator SpotlightsRotate()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 4; i++)
        {
            spotlights[i].SetActive(true);
        }

        spotlightAnimator.Play("Spotlights Rotation");
        yield return null;
    }

    IEnumerator TurnOnSpotlightByRound()
    {
        yield return new WaitForSeconds(1f);
        spotlightOn.Play();
        spotlights[currentRound].SetActive(true);
        yield return null;
    }

    IEnumerator DelayedRefresh(GameObject obj)
    {
        yield return new WaitForSeconds(6f); //wait for a whole second to allow refresh; //waiting for fixed update wasnt long enough

        foreach (var col in obj.GetComponentsInChildren<Collider>(true))
        {
            RefreshCollider(col);
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
