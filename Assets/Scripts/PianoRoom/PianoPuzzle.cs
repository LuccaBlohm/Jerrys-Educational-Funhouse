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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateSpotlights());
        panelEnd = leverPanel.transform.position - new Vector3(0, 17.33f, 0);
    }

    // Update is called once per frame
    void Update()
    {

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

        yield return new WaitForSeconds(1.5f);
        spotlights[currentRound].SetActive(true);
    }

    IEnumerator MoveLeverPanel()
    {
        while (Vector3.Distance(leverPanel.transform.position, panelEnd) > 0.01f)
        {
            leverPanel.transform.position = Vector3.Lerp(leverPanel.transform.position, panelEnd, Time.deltaTime * slideSpeed);
            yield return null;
        }
    }

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
}
